# 💬 RealTimeChat

**RealTimeChat** is a lightweight, real-time chat application built with **ASP.NET Core**, **SignalR**, and **Azure Cosmos DB**.  
It demonstrates how to implement a real-time communication system with reliable message persistence and scalable cloud integration — all packaged in a simple, container-ready application.

🚀 **Live Demo:** [https://ccl-chatapp.onrender.com](https://ccl-chatapp.onrender.com)

---

## ✨ Features

- ⚡ **Real-time messaging:** Powered by SignalR, messages appear instantly across all connected clients.  
- ☁️ **Persistent message storage:** All chat messages are stored in Azure Cosmos DB to ensure reliability and history preservation.  
- 🧩 **Configurable & Cloud-ready:** Easily configurable via `appsettings.json` or environment variables.  
- 📦 **Dockerized:** Ready to run locally or deploy as a containerized application.  
- 🌐 **Hosted on Render:** Deployed on Render for seamless, scalable hosting.

---

## 🧰 Tech Stack

- **Backend:** ASP.NET Core (.NET 7)  
- **Real-time Communication:** SignalR  
- **Database:** Azure Cosmos DB (Core/SQL API)  
- **Cloud Hosting:** Render  
- **Containerization:** Docker

---

## 🏃 Quick Start — Run Locally

### 1. Clone the repository

```bash
git clone https://github.com/yourusername/RealTimeChat.git
cd RealTimeChat
```

### 2. Restore and build the project

```powershell
dotnet restore
dotnet build
```

### 3. Run the application

```powershell
dotnet run
```

The application will start and serve the web UI from `wwwroot/index.html`.  
SignalR endpoints are exposed automatically and used by the front end.

Visit: [http://localhost:5000](http://localhost:5000)

---

## ☁️ Cloud Services Used

### 🛰️ 1. **Azure SignalR Service**

Azure SignalR Service is a fully managed service that simplifies adding real-time web functionality to applications.  
In this project, it:

- Handles **real-time message delivery** across all connected users.  
- Manages **connection scaling** automatically, so the chat app can support multiple concurrent users without extra infrastructure.  
- Keeps **latency low**, ensuring messages are delivered instantly.

📌 **Configuration:**  
The connection string is stored in configuration:
```json
"Azure": {
  "SignalR": {
    "ConnectionString": "Your Azure SignalR Connection String"
  }
}
```
or as an environment variable:
```
Azure__SignalR__ConnectionString
```

---

### 🗃️ 2. **Azure Cosmos DB**

Azure Cosmos DB is a globally distributed, NoSQL database.  
In this application, it:

- Stores **chat messages** and any related metadata.  
- Provides **low-latency reads and writes**, ensuring messages are persisted in near real time.  
- Enables **horizontal scalability** for future growth and analytics.

📌 **Configuration:**  
The connection string is stored in:
```json
"Azure": {
  "CosmosDb": {
    "ConnectionString": "Your Azure Cosmos DB Connection String"
  }
}
```
or as an environment variable:
```
Azure__CosmosDb__ConnectionString
```

> 💡 **Why Cosmos DB?**  
> Unlike a traditional database, Cosmos DB is designed for global scale, making it ideal for chat systems where data needs to be stored and retrieved quickly and reliably.

---

### ☁️ 3. **Render (Cloud Hosting Platform)**

Render is a modern cloud hosting platform used to **deploy and host** this chat application.  
It offers:

- ⚙️ **Zero-downtime deployments** — updates are automatically rolled out.  
- 🌍 **Global availability** — ensuring the app is accessible worldwide.  
- 🔐 **SSL & HTTPS** out of the box — no manual setup required.  
- 🪄 Simple deployment from GitHub or Docker image.

🖥️ **Hosted URL:** [https://ccl-chatapp.onrender.com](https://ccl-chatapp.onrender.com)

---

## 🐳 Running in Docker

To run the application in a container:

```bash
docker build -t realtimechat .
docker run -p 8080:80 realtimechat
```

The application will be available at [http://localhost:8080](http://localhost:8080).

---
