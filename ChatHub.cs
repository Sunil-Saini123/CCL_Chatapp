using Microsoft.AspNetCore.SignalR;
using Microsoft.Azure.Cosmos;
using Newtonsoft.Json;

public class ChatHub : Hub
{
    private readonly CosmosClient _cosmosClient;
    private readonly ILogger<ChatHub> _logger;
    private static readonly HashSet<string> ActiveRooms = new HashSet<string>();
    private static readonly Dictionary<string, HashSet<string>> RoomUsers = new Dictionary<string, HashSet<string>>();
    private static readonly Dictionary<string, string> UserRooms = new Dictionary<string, string>();

    public ChatHub(CosmosClient cosmosClient, ILogger<ChatHub> logger)
    {
        _cosmosClient = cosmosClient;
        _logger = logger;
    }

    public async Task CreateRoom(string roomId)
    {
        try
        {
            _logger.LogInformation("Client {ConnectionId} creating room {RoomId}", Context.ConnectionId, roomId);
            
            bool roomCreated = false;
            lock (ActiveRooms)
            {
                if (!ActiveRooms.Contains(roomId))
                {
                    ActiveRooms.Add(roomId);
                    lock (RoomUsers)
                    {
                        RoomUsers[roomId] = new HashSet<string>();
                    }
                    roomCreated = true;
                    _logger.LogInformation("Room {RoomId} created successfully", roomId);
                }
                else
                {
                    _logger.LogInformation("Room {RoomId} already exists", roomId);
                }
            }

            // Get a snapshot of current rooms under the lock
            List<string> currentRooms;
            lock (ActiveRooms)
            {
                currentRooms = ActiveRooms.ToList();
            }

            // Send to creator immediately
            await Clients.Caller.SendAsync("AvailableRooms", currentRooms);
            
            // Then to everyone else
            await Clients.Others.SendAsync("AvailableRooms", currentRooms);
            
            _logger.LogInformation("Room list broadcast complete. Active rooms: {Rooms}", 
                string.Join(", ", currentRooms));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating room {RoomId}", roomId);
            throw;
        }
    }

    private async Task BroadcastAvailableRooms()
    {
        try
        {
            List<string> rooms;
            lock (ActiveRooms)
            {
                rooms = ActiveRooms.ToList();
            }
            
            if (rooms.Any())
            {
                _logger.LogInformation("Broadcasting available rooms: {Rooms}", string.Join(", ", rooms));
            }
            else
            {
                _logger.LogInformation("Broadcasting empty room list");
            }

            // First send to the caller to ensure they get the update
            await Clients.Caller.SendAsync("AvailableRooms", rooms);
            // Then broadcast to everyone else
            await Clients.Others.SendAsync("AvailableRooms", rooms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error broadcasting available rooms");
            throw;
        }
    }

    public async Task JoinRoom(string roomId, string username)
    {
        try
        {
            _logger.LogInformation("User {Username} attempting to join room {RoomId}", username, roomId);
            
            // Leave current room if user is in one
            if (UserRooms.TryGetValue(Context.ConnectionId, out var currentRoom))
            {
                await LeaveRoom(currentRoom, username);
            }

            lock (ActiveRooms)
            {
                // Add room to active rooms if it doesn't exist
                if (!ActiveRooms.Contains(roomId))
                {
                    ActiveRooms.Add(roomId);
                    RoomUsers[roomId] = new HashSet<string>();
                    _logger.LogInformation("Created new room {RoomId}", roomId);
                }
            }

            lock (RoomUsers)
            {
                // Add user to room's user list
                if (!RoomUsers.ContainsKey(roomId))
                {
                    RoomUsers[roomId] = new HashSet<string>();
                }
                RoomUsers[roomId].Add(username);
                UserRooms[Context.ConnectionId] = roomId;
            }
            
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
            _logger.LogInformation("Added user {Username} to SignalR group {RoomId}", username, roomId);

            // Broadcast updated room list to all clients
            await BroadcastAvailableRooms();
            
            // Broadcast updated user list to everyone in the room
            await BroadcastUserList(roomId);
            
            // Notify room about the new user
            await Clients.Group(roomId).SendAsync("UserJoined", username);
            _logger.LogInformation("Notified room {RoomId} about user {Username} joining", roomId, username);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while user {Username} was joining room {RoomId}", username, roomId);
            throw;
        }
    }

    private async Task BroadcastUserList(string roomId)
    {
        try
        {
            if (RoomUsers.TryGetValue(roomId, out var users))
            {
                var userList = users.ToList(); // Create a copy to avoid threading issues
                _logger.LogInformation("Broadcasting user list for room {RoomId}: {Users}", 
                    roomId, string.Join(", ", userList));
                await Clients.Group(roomId).SendAsync("UpdateUserList", userList);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error broadcasting user list for room {RoomId}", roomId);
            throw;
        }
    }

    public async Task LeaveRoom(string roomId, string username)
    {
        try
        {
            _logger.LogInformation("User {Username} leaving room {RoomId}", username, roomId);
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
            
            bool roomRemoved = false;
            lock (RoomUsers)
            {
                if (RoomUsers.ContainsKey(roomId))
                {
                    RoomUsers[roomId].Remove(username);
                    if (RoomUsers[roomId].Count == 0)
                    {
                        lock (ActiveRooms)
                        {
                            ActiveRooms.Remove(roomId);
                        }
                        RoomUsers.Remove(roomId);
                        roomRemoved = true;
                        _logger.LogInformation("Removed empty room {RoomId}", roomId);
                    }
                }
                UserRooms.Remove(Context.ConnectionId);
            }
            
            if (!roomRemoved)
            {
                // Only notify remaining users if the room still exists
                await Clients.Group(roomId).SendAsync("UserLeft", username);
                await BroadcastUserList(roomId);
            }

            // Always broadcast available rooms to everyone
            await BroadcastAvailableRooms();
            
            _logger.LogInformation("User {Username} left room {RoomId}", username, roomId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while user {Username} was leaving room {RoomId}", username, roomId);
            throw;
        }
    }

    public override async Task OnConnectedAsync()
    {
        try
        {
            await base.OnConnectedAsync();
            
            // Send the current room list directly to the new client
            List<string> rooms;
            lock (ActiveRooms)
            {
                rooms = ActiveRooms.ToList();
            }
            
            await Clients.Caller.SendAsync("AvailableRooms", rooms);
            _logger.LogInformation("New client connected with ID: {ConnectionId}. Sent room list: {Rooms}", 
                Context.ConnectionId,
                rooms.Any() ? string.Join(", ", rooms) : "<empty>");

            // Also broadcast to others to ensure everyone is in sync
            await Clients.Others.SendAsync("AvailableRooms", rooms);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in OnConnectedAsync");
            throw;
        }
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        try
        {
            string? roomId = null;
            string? username = null;

            lock (UserRooms)
            {
                if (UserRooms.TryGetValue(Context.ConnectionId, out roomId))
                {
                    lock (RoomUsers)
                    {
                        if (RoomUsers.TryGetValue(roomId, out var users))
                        {
                            username = users.FirstOrDefault(u => UserRooms.Any(ur => ur.Key == Context.ConnectionId));
                        }
                    }
                }
            }

            if (roomId != null && username != null)
            {
                _logger.LogInformation("User {Username} disconnected from room {RoomId}", username, roomId);
                await LeaveRoom(roomId, username);
            }
            else
            {
                _logger.LogWarning("User disconnected but room or username not found. ConnectionId: {ConnectionId}", Context.ConnectionId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in OnDisconnectedAsync. ConnectionId: {ConnectionId}", Context.ConnectionId);
        }
        finally
        {
            await base.OnDisconnectedAsync(exception);
        }
    }

    public async Task RequestRoomList()
    {
        try
        {
            await BroadcastAvailableRooms();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in RequestRoomList");
            throw;
        }
    }

    public async Task UserTyping(string roomId, string username)
    {
        await Clients.Group(roomId).SendAsync("UserTyping", username);
    }

    public async Task SendMessage(string roomId, string username, string message)
    {
        try
        {
            _logger.LogInformation("Attempting to send message from {Username} in room {RoomId}", username, roomId);

            // First, send the message to clients immediately for responsive UI
            var timestamp = DateTime.UtcNow;
            await Clients.Group(roomId).SendAsync("ReceiveMessage", username, message, timestamp);

            // Then try to save to Cosmos DB if available
            if (_cosmosClient != null)
            {
                try
                {
                    var container = _cosmosClient.GetDatabase("ChatDb").GetContainer("Messages");
                    
                    var chatMessage = new ChatMessage
                    {
                        roomid = roomId,
                        username = username,
                        content = message,
                        timestamp = timestamp
                    };

                    await container.CreateItemAsync(chatMessage, new PartitionKey(roomId));
                    _logger.LogInformation("Successfully saved message to Cosmos DB");
                }
                catch (Exception ex)
                {
                    // Log the error but don't throw - message was already delivered to clients
                    _logger.LogWarning("Failed to save message to Cosmos DB (message was delivered to clients): {Error}", ex.Message);
                }
            }
            else
            {
                _logger.LogInformation("Message not persisted - Cosmos DB client not available");
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SendMessage for user {Username} in room {RoomId}", username, roomId);
            throw;
        }
    }
}

public class ChatMessage
{
    public ChatMessage()
    {
        id = Guid.NewGuid().ToString();
        roomid = string.Empty;
        username = string.Empty;
        content = string.Empty;
        timestamp = DateTime.UtcNow;
    }

    [JsonProperty(PropertyName = "id")]
    public string id { get; set; }

    [JsonProperty(PropertyName = "roomid")]
    public string roomid { get; set; }

    [JsonProperty(PropertyName = "username")]
    public string username { get; set; }

    [JsonProperty(PropertyName = "content")]
    public string content { get; set; }

    [JsonProperty(PropertyName = "timestamp")]
    public DateTime timestamp { get; set; }
}