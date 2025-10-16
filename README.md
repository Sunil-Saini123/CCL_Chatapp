# Real-Time Chat Application# Real-Time Chat Application# Real-Time Chat Application



A modern real-time chat application built with ASP.NET Core, featuring room-based conversations, real-time messaging, user presence tracking, and persistent message storage using cloud services.



## 🎯 What is this Project?A modern real-time chat application built with ASP.NET Core, featuring room-based conversations, real-time messaging, user presence tracking, and persistent message storage using cloud services.A modern real-time chat application built with ASP.NET Core and Azure cloud services, featuring room-based conversations, real-time presence tracking, and message persistence.



This is a **real-time chat application** that allows users to:

- Create and join multiple chat rooms

- Send and receive messages instantly## 🎯 What is this Project?## Azure Services Integration

- See who's online in each room

- View typing indicators when others are typing

- Switch between dark and light themes

- Use emojis in messagesThis is a **real-time chat application** that allows users to:### 1. Azure SignalR Service

- Access chat history (messages are saved)

- Create and join multiple chat rooms- **Purpose**: Manages real-time communication infrastructure

The application provides a modern, responsive user interface built with HTML and Tailwind CSS, and leverages enterprise-grade cloud services for scalability and reliability.

- Send and receive messages instantly- **Features**:

## ☁️ Cloud Services Used

- See who's online in each room  - Handles WebSocket connections at scale

### Azure Services

- View typing indicators when others are typing  - Manages client connection state

1. **Azure SignalR Service (Free Tier)**

   - Manages real-time WebSocket connections- Switch between dark and light themes  - Provides automatic failover and recovery

   - Handles instant message delivery to all users

   - Supports up to 20 concurrent connections on free tier- Use emojis in messages  - Enables real-time message broadcasting

   - Provides automatic scaling and failover

- Access chat history (messages are saved)- **Implementation**:

2. **Azure Cosmos DB (NoSQL)**

   - Stores all chat messages persistently  - Used for room management

   - Enables message history retrieval

   - Uses room-based partitioning for efficient queriesThe application provides a modern, responsive user interface built with HTML and Tailwind CSS, and leverages enterprise-grade cloud services for scalability and reliability.  - Handles user presence tracking

   - Serverless pricing (pay only for what you use)

  - Manages typing indicators

### Hosting Service

## ☁️ Cloud Services Used  - Delivers instant messages

3. **Render.com (Free Tier)**

   - Hosts the ASP.NET Core web application  - Provides connection resilience

   - Provides automatic deployments from GitHub

   - Includes free SSL certificates### Azure Services

   - Docker-based containerization

   - **Note:** Free tier spins down after 15 minutes of inactivity (takes ~30 seconds to wake up)1. **Azure SignalR Service (Free Tier)**- **Purpose**: Provides scalable message persistence



## 🚀 How to Run   - Manages real-time WebSocket connections- **Features**:



### Option 1: Access the Hosted Version   - Handles instant message delivery to all users  - Stores chat messages with room-based partitioning



Simply visit the live application:   - Supports up to 20 concurrent connections on free tier  - Enables message history retrieval



**🔗 [https://ccl-chatapp.onrender.com](https://ccl-chatapp.onrender.com)**   - Provides automatic scaling and failover  - Provides automatic indexing



1. Enter your username  - Offers multi-region support

2. Create a new room or join an existing one

3. Start chatting!2. **Azure Cosmos DB (NoSQL)**- **Implementation**:



> ⚠️ **Note:** First load may take 30-60 seconds if the service was idle (free tier limitation)   - Stores all chat messages persistently  - Messages stored by room (partition key)



### Option 2: Run Locally   - Enables message history retrieval  - Maintains chat history



#### Prerequisites   - Uses room-based partitioning for efficient queries  - Enables message querying

- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or later

- Azure SignalR Service and Cosmos DB connection strings (or create your own free tier resources)   - Serverless pricing (pay only for what you use)  - Supports offline message access



#### Steps



1. **Clone the repository**### Hosting Service### 3. Azure Key Vault (Production)

   ```bash

   git clone https://github.com/Sunil-Saini123/CCL_Chatapp.git- **Purpose**: Secures application configuration

   cd CCL_Chatapp

   ```3. **Render.com (Free Tier)**- **Features**:



2. **Configure Azure connection strings**   - Hosts the ASP.NET Core web application  - Manages service connection strings

   

   Create or update `appsettings.json`:   - Provides automatic deployments from GitHub  - Stores application secrets

   ```json

   {   - Includes free SSL certificates  - Provides identity-based access

     "Azure": {

       "SignalR": {   - Docker-based containerization- **Implementation**:

         "ConnectionString": "YOUR_AZURE_SIGNALR_CONNECTION_STRING"

       },   - **Note:** Free tier spins down after 15 minutes of inactivity (takes ~30 seconds to wake up)  - Secures SignalR connection string

       "CosmosDb": {

         "ConnectionString": "YOUR_AZURE_COSMOSDB_CONNECTION_STRING"  - Protects Cosmos DB credentials

       }

     }## 🚀 How to Run  - Manages service principals

   }

   ```  - Controls secret access



3. **Run the application**### Option 1: Access the Hosted Version

   ```bash

   dotnet run## Core Features

   ```

Simply visit the live application:

4. **Open in browser**

   - Real-time messaging with SignalR Service

   Navigate to: `http://localhost:5000`

**🔗 [https://ccl-chatapp.onrender.com](https://ccl-chatapp.onrender.com)**- Persistent storage with Cosmos DB

That's it! The application is now running locally and connected to Azure cloud services.

- Multiple chat rooms with real-time updates

---

1. Enter your username- User presence and typing indicators

## 📝 Features

2. Create a new room or join an existing one- Join/leave notifications

- ✅ Real-time messaging with SignalR

- ✅ Multiple chat rooms3. Start chatting!- Responsive Tailwind CSS design

- ✅ User presence tracking

- ✅ Typing indicators- Dark/Light theme support

- ✅ Message persistence

- ✅ Dark/Light theme toggle> ⚠️ **Note:** First load may take 30-60 seconds if the service was idle (free tier limitation)- Emoji picker integration

- ✅ Emoji picker

- ✅ Responsive design

- ✅ Room management (create/join/leave)

### Option 2: Run Locally## Prerequisites

## 🛠️ Tech Stack



- **Frontend:** HTML, JavaScript, Tailwind CSS

- **Backend:** ASP.NET Core 7.0, C##### Prerequisites1. Azure Account with the following services:

- **Real-time Communication:** Azure SignalR Service

- **Database:** Azure Cosmos DB (NoSQL)- [.NET 7.0 SDK](https://dotnet.microsoft.com/download/dotnet/7.0) or later   - Azure SignalR Service

- **Hosting:** Render.com (Docker)

- **Version Control:** GitHub- Azure SignalR Service and Cosmos DB connection strings (or create your own free tier resources)   - Azure Cosmos DB




#### Steps2. .NET 7.0 SDK or later



1. **Clone the repository**## Configuration

   ```bash

   git clone https://github.com/Sunil-Saini123/CCL_Chatapp.git1. Update `appsettings.json` with your Azure service connection strings:

   cd CCL_Chatapp

   ``````json

{

2. **Configure Azure connection strings**  "Azure": {

       "SignalR": {

   Create or update `appsettings.json`:      "ConnectionString": "YOUR_SIGNALR_CONNECTION_STRING"

   ```json    },

   {    "CosmosDb": {

     "Azure": {      "ConnectionString": "YOUR_COSMOSDB_CONNECTION_STRING"

       "SignalR": {    }

         "ConnectionString": "YOUR_AZURE_SIGNALR_CONNECTION_STRING"  }

       },}

       "CosmosDb": {```

         "ConnectionString": "YOUR_AZURE_COSMOSDB_CONNECTION_STRING"

       }## Running the Application

     }

   }1. Clone the repository

   ```2. Update the connection strings in `appsettings.json`

3. Run the application:

3. **Run the application**   ```

   ```bash   dotnet run

   dotnet run   ```

   ```4. Open your browser and navigate to `http://localhost:5000`



4. **Open in browser**## Usage

   

   Navigate to: `http://localhost:5000`1. Enter your username and a room ID

2. Click "Join Room" to enter the chat

That's it! The application is now running locally and connected to Azure cloud services.3. Send messages using the input field at the bottom

4. Click "Leave Room" to exit

---

## Architecture

### 📝 Features

```

- ✅ Real-time messaging with SignalR┌─────────────┐     ┌─────────────┐     ┌─────────────┐

- ✅ Multiple chat rooms│   Browser   │◄────┤ Azure       │◄────┤   ASP.NET   │

- ✅ User presence tracking│   Client    │     │ SignalR     │     │   Core App  │

- ✅ Typing indicators└─────────────┘     └─────────────┘     └──────┬──────┘

- ✅ Message persistence                                               │

- ✅ Dark/Light theme toggle                    ┌─────────────┐     ┌──────┴──────┐

- ✅ Emoji picker                    │    Key      │◄────┤    Azure    │

- ✅ Responsive design                    │   Vault     │     │  Cosmos DB  │

- ✅ Room management (create/join/leave)                    └─────────────┘     └─────────────┘

```

### 🛠️ Tech Stack

### Components

- **Frontend:** HTML, JavaScript, Tailwind CSS- **Frontend**: HTML, JavaScript, Tailwind CSS

- **Backend:** ASP.NET Core 7.0, C#- **Backend**: ASP.NET Core 7.0

- **Real-time Communication:** Azure SignalR Service- **Real-time Communication**: Azure SignalR Service

- **Database:** Azure Cosmos DB (NoSQL)- **Data Storage**: Azure Cosmos DB

- **Hosting:** Render.com (Docker)- **Security**: Azure Key Vault (Production)

- **Version Control:** GitHub

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

