# Real-Time Chat Application

A modern real-time chat application built with ASP.NET Core and Azure cloud services, featuring room-based conversations, real-time presence tracking, and message persistence.

## Azure Services Integration

### 1. Azure SignalR Service
- **Purpose**: Manages real-time communication infrastructure
- **Features**:
  - Handles WebSocket connections at scale
  - Manages client connection state
  - Provides automatic failover and recovery
  - Enables real-time message broadcasting
- **Implementation**:
  - Used for room management
  - Handles user presence tracking
  - Manages typing indicators
  - Delivers instant messages
  - Provides connection resilience

### 2. Azure Cosmos DB
- **Purpose**: Provides scalable message persistence
- **Features**:
  - Stores chat messages with room-based partitioning
  - Enables message history retrieval
  - Provides automatic indexing
  - Offers multi-region support
- **Implementation**:
  - Messages stored by room (partition key)
  - Maintains chat history
  - Enables message querying
  - Supports offline message access

### 3. Azure Key Vault (Production)
- **Purpose**: Secures application configuration
- **Features**:
  - Manages service connection strings
  - Stores application secrets
  - Provides identity-based access
- **Implementation**:
  - Secures SignalR connection string
  - Protects Cosmos DB credentials
  - Manages service principals
  - Controls secret access

## Core Features

- Real-time messaging with SignalR Service
- Persistent storage with Cosmos DB
- Multiple chat rooms with real-time updates
- User presence and typing indicators
- Join/leave notifications
- Responsive Tailwind CSS design
- Dark/Light theme support
- Emoji picker integration

## Prerequisites

1. Azure Account with the following services:
   - Azure SignalR Service
   - Azure Cosmos DB

2. .NET 7.0 SDK or later

## Configuration

1. Update `appsettings.json` with your Azure service connection strings:

```json
{
  "Azure": {
    "SignalR": {
      "ConnectionString": "YOUR_SIGNALR_CONNECTION_STRING"
    },
    "CosmosDb": {
      "ConnectionString": "YOUR_COSMOSDB_CONNECTION_STRING"
    }
  }
}
```

## Running the Application

1. Clone the repository
2. Update the connection strings in `appsettings.json`
3. Run the application:
   ```
   dotnet run
   ```
4. Open your browser and navigate to `http://localhost:5000`

## Usage

1. Enter your username and a room ID
2. Click "Join Room" to enter the chat
3. Send messages using the input field at the bottom
4. Click "Leave Room" to exit

## Architecture

```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Browser   │◄────┤ Azure       │◄────┤   ASP.NET   │
│   Client    │     │ SignalR     │     │   Core App  │
└─────────────┘     └─────────────┘     └──────┬──────┘
                                               │
                    ┌─────────────┐     ┌──────┴──────┐
                    │    Key      │◄────┤    Azure    │
                    │   Vault     │     │  Cosmos DB  │
                    └─────────────┘     └─────────────┘
```

### Components
- **Frontend**: HTML, JavaScript, Tailwind CSS
- **Backend**: ASP.NET Core 7.0
- **Real-time Communication**: Azure SignalR Service
- **Data Storage**: Azure Cosmos DB
- **Security**: Azure Key Vault (Production)

### Data Flow
1. Client connects via WebSocket through Azure SignalR Service
2. Messages are broadcast in real-time to room participants
3. Messages are persisted to Cosmos DB for history
4. Room state is managed in-memory with SignalR groups
5. Configuration and secrets are managed by Key Vault

## Performance Considerations

- SignalR Service handles connection scaling
- Cosmos DB partitioning by room for query efficiency
- Client-side caching of room state
- Optimized room list synchronization
- Automatic reconnection handling
- Connection state recovery

## Deploying to Render (Free Hosting) with Azure Services

Render provides free hosting for your ASP.NET Core application while you continue using Azure SignalR Service and Cosmos DB for backend services. This gives you the best of both worlds - free hosting and enterprise-grade Azure services.

### Why This Setup?

✅ **Free hosting on Render** - No payment for the web app hosting  
✅ **Azure SignalR Service** - Scalable real-time communication  
✅ **Azure Cosmos DB** - Reliable message persistence  
✅ **Easy deployment** - Push to GitHub and auto-deploy  
✅ **Built-in SSL** - Free HTTPS certificates from Render  

### Architecture

```
┌─────────────┐     ┌─────────────┐     ┌─────────────┐
│   Browser   │◄────┤   Azure     │◄────┤   Render    │
│   Client    │     │   SignalR   │     │  (Hosting)  │
└─────────────┘     └─────────────┘     └──────┬──────┘
                                               │
                                        ┌──────┴──────┐
                                        │    Azure    │
                                        │  Cosmos DB  │
                                        └─────────────┘
```

### Step 1: Set Up Azure Services (One-time Setup)

#### Create Azure SignalR Service

```bash
# Login to Azure
az login

# Create a resource group
az group create --name RealTimeChatRG --location eastus

# Create Azure SignalR Service (Basic tier - cheapest option)
az signalr create --name <your-signalr-name> \
  --resource-group RealTimeChatRG \
  --sku Free_F1 \
  --service-mode Default

# Get the connection string
az signalr key list --name <your-signalr-name> \
  --resource-group RealTimeChatRG \
  --query primaryConnectionString -o tsv
```

**Save the connection string** - you'll need it for Render!

#### Create Azure Cosmos DB

```bash
# Create Cosmos DB account (use serverless for cost savings)
az cosmosdb create --name <your-cosmosdb-name> \
  --resource-group RealTimeChatRG \
  --default-consistency-level Session \
  --locations regionName=eastus failoverPriority=0 \
  --capabilities EnableServerless

# Create database
az cosmosdb sql database create \
  --account-name <your-cosmosdb-name> \
  --resource-group RealTimeChatRG \
  --name ChatDb

# Create container
az cosmosdb sql container create \
  --account-name <your-cosmosdb-name> \
  --resource-group RealTimeChatRG \
  --database-name ChatDb \
  --name Messages \
  --partition-key-path /roomid

# Get the connection string
az cosmosdb keys list --name <your-cosmosdb-name> \
  --resource-group RealTimeChatRG \
  --type connection-strings \
  --query "connectionStrings[0].connectionString" -o tsv
```

**Save the connection string** - you'll need it for Render!

### Step 2: Prepare Your Application for Render

Your application is already configured correctly! Just ensure your `Program.cs` has:

```csharp
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
var builder = WebApplication.CreateBuilder(args);

// ... rest of your configuration

var app = builder.Build();

// Important: Listen on the PORT environment variable
app.Run($"http://0.0.0.0:{port}");
```

If not present, add this at the end of `Program.cs`:

```csharp
var port = Environment.GetEnvironmentVariable("PORT") ?? "5000";
app.Run($"http://0.0.0.0:{port}");
```

### Step 3: Push Code to GitHub

```bash
# Initialize git (if not already done)
git init

# Create .gitignore to exclude unnecessary files
echo "bin/
obj/
.vs/
*.user
appsettings.Development.json
appsettings.json" > .gitignore

# Commit your code
git add .
git commit -m "Prepare for Render deployment"

# Create repository on GitHub and push
git remote add origin https://github.com/yourusername/realtimechat.git
git branch -M main
git push -u origin main
```

### Step 4: Deploy on Render

#### 1. Sign Up for Render
- Go to [render.com](https://render.com)
- Sign up using GitHub
- **No credit card required for free tier!**

#### 2. Create New Web Service
- Click **"New +"** button in dashboard
- Select **"Web Service"**
- Click **"Connect account"** to link GitHub
- Select your `realtimechat` repository
- Click **"Connect"**

#### 3. Configure the Service

Fill in these settings:

| Setting | Value |
|---------|-------|
| **Name** | `realtimechat` (or your preferred name) |
| **Region** | Choose closest to you |
| **Branch** | `main` |
| **Runtime** | `.NET` |
| **Build Command** | `dotnet publish -c Release -o out` |
| **Start Command** | `dotnet out/RealTimeChat.dll` |
| **Instance Type** | **Free** |

#### 4. Add Environment Variables

Click **"Advanced"** and add these environment variables:

| Key | Value |
|-----|-------|
| `Azure__SignalR__ConnectionString` | Your Azure SignalR connection string |
| `Azure__CosmosDb__ConnectionString` | Your Cosmos DB connection string |
| `ASPNETCORE_ENVIRONMENT` | `Production` |

**Important:** Use the exact format with double underscores (`__`) to match the configuration hierarchy!

#### 5. Deploy

- Click **"Create Web Service"**
- Render will automatically:
  - Clone your repository
  - Build the application
  - Deploy it
  - Assign a URL (e.g., `https://realtimechat.onrender.com`)

⏱️ **First deployment takes 5-10 minutes**

### Step 5: Access Your Application

Once deployed, your app will be available at:
```
https://your-app-name.onrender.com
```

🎉 **Your chat application is now live!**

### Cost Breakdown

| Service | Tier | Cost |
|---------|------|------|
| **Render Hosting** | Free | $0/month |
| **Azure SignalR** | Free F1 | $0/month (20 concurrent connections) |
| **Azure Cosmos DB** | Serverless | ~$0.25-5/month (pay per use) |
| **Total** | | **~$0-5/month** |

💡 **Note:** Cosmos DB serverless only charges for actual usage (requests + storage), so costs are minimal for small projects.

### Automatic Deployments

Every time you push to GitHub, Render automatically rebuilds and deploys:

```bash
# Make changes to your code
git add .
git commit -m "Add new feature"
git push origin main

# Render automatically detects the push and redeploys! 🚀
```

### Monitoring Your Application

In the Render dashboard, you can:
- **View Logs**: Real-time application logs
- **See Metrics**: CPU, memory usage
- **Check Events**: Deployment history
- **Access Shell**: Debug via terminal

### Important Notes About Free Tier

⚠️ **Render Free Tier Limitations:**
- Service **spins down after 15 minutes** of inactivity
- First request after spin-down takes **30-60 seconds** to wake up
- 750 hours/month (enough for one service running 24/7)

💡 **Tips:**
- Perfect for development/testing
- Upgrade to **Starter ($7/month)** for always-on service
- Service automatically wakes up on any incoming request

### Scaling Azure Services

If you need more capacity later:

```bash
# Upgrade SignalR to Standard tier (1,000 concurrent connections)
az signalr update --name <your-signalr-name> \
  --resource-group RealTimeChatRG \
  --sku Standard_S1

# Cosmos DB serverless auto-scales, no action needed!
```

### Troubleshooting

#### Issue: Service won't start
**Solution:** Check logs in Render dashboard for errors

#### Issue: Can't connect to SignalR
**Solution:** Verify environment variable name:
- Must be: `Azure__SignalR__ConnectionString`
- Not: `Azure:SignalR:ConnectionString`

#### Issue: Cosmos DB connection fails
**Solution:** 
1. Check connection string format
2. Ensure Cosmos DB firewall allows connections from `0.0.0.0/0` (all IPs)

```bash
# Allow connections from anywhere
az cosmosdb update --name <your-cosmosdb-name> \
  --resource-group RealTimeChatRG \
  --enable-public-network true
```

#### Issue: Static files not loading
**Solution:** Ensure `wwwroot` folder is committed to GitHub

### Upgrade to Always-On

When ready for production:

1. **Upgrade Render**:
   - Go to service settings
   - Change plan to **Starter** ($7/month)
   - Service stays always-on

2. **Monitor Costs**:
   - Azure SignalR Free: Up to 20 concurrent connections
   - Cosmos DB Serverless: Pay only for what you use
   - Render Starter: Fixed $7/month

### Deployment Checklist

- [ ] Azure SignalR Service created
- [ ] Azure Cosmos DB created (serverless mode)
- [ ] Connection strings saved
- [ ] Code pushed to GitHub
- [ ] Render account created
- [ ] Web Service configured
- [ ] Environment variables added
- [ ] Build and start commands set
- [ ] Free tier selected
- [ ] Deployment successful
- [ ] App accessible at Render URL

🎉 **You now have a production-ready chat app with minimal costs!**

### Next Steps

- Add custom domain in Render settings
- Enable automatic SSL (included free)
- Set up monitoring with Application Insights
- Configure auto-scaling for Azure services
- Implement user authentication

