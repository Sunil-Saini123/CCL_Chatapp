using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSignalR().AddAzureSignalR(options =>
{
    options.ConnectionString = builder.Configuration["Azure:SignalR:ConnectionString"];
});

// Add Cosmos DB client
builder.Services.AddSingleton(serviceProvider =>
{
    var connectionString = builder.Configuration["Azure:CosmosDb:ConnectionString"];
    var client = new CosmosClient(connectionString);
    var database = client.CreateDatabaseIfNotExistsAsync("ChatDb").GetAwaiter().GetResult();
    var container = database.Database.CreateContainerIfNotExistsAsync("Messages", "/roomId").GetAwaiter().GetResult();
    return client;
});

var app = builder.Build();

app.UseStaticFiles();
app.UseRouting();

app.MapHub<ChatHub>("/chat");
app.MapGet("/", () => Results.File("wwwroot/index.html", "text/html"));

app.Run();