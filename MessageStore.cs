public interface IMessageStore
{
    Task SaveMessageAsync(ChatMessage message);
    Task<IEnumerable<ChatMessage>> GetMessagesForRoomAsync(string roomId);
}

public class InMemoryMessageStore : IMessageStore
{
    private readonly List<ChatMessage> _messages = new();
    private readonly object _lock = new();

    public Task SaveMessageAsync(ChatMessage message)
    {
        lock (_lock)
        {
            _messages.Add(message);
        }
        return Task.CompletedTask;
    }

    public Task<IEnumerable<ChatMessage>> GetMessagesForRoomAsync(string roomId)
    {
        lock (_lock)
        {
            return Task.FromResult(_messages.Where(m => m.roomid == roomId));
        }
    }
}