# 🏦 Cash Flow Control

**Cash Flow Control** is a financial tracking system that helps merchants manage their daily cash flow with transaction logging (debits and credits) and a consolidated daily balance report.

## 🚀 Features
- **Transaction Management**: Create, list, and retrieve financial transactions.
- **Daily Consolidation Service**: Processes and consolidates daily financial records asynchronously.
- **Resilient Architecture**: Ensures the transaction service remains available even if the consolidation service fails.
- **Scalability**: Handles up to 50 requests per second with a maximum 5% request loss.

## 🛠 Tech Stack
- **C# with .NET 8**
- **Entity Framework Core** for data persistence
- **MediatR** for internal communication
- **RabbitMQ or Apache Kafka** for async processing
- **Docker & Docker Compose** for easy deployment
- **Swagger** for API documentation
- **xUnit/NUnit** for unit and integration testing
- **Serilog** for structured logging

## 📦 Project Structure
```
📦 CashFlowControl  
 ┣ 📂 src  
 ┃ ┣ 📂 LaunchesService (Transaction Service)  
 ┃ ┣ 📂 ConsolidationService (Daily Consolidation Service)  
 ┃ ┣ 📂 SharedKernel (Common domain logic)  
 ┃ ┗ 📂 Tests (Unit and integration tests)  
 ┣ 📜 README.md (Project documentation)  
 ┣ 📜 docker-compose.yml (Environment configuration)  
 ┗ 📜 CashFlowControl.sln (Solution file)  
```

## 📖 Setup & Usage
1. Clone the repository:  
   ```sh  
   git clone https://github.com/Brand00wn/CashFlowControl.git
   cd cashflow-control  
   ```  
2. Build and run the services using Docker:  
   ```sh  
   docker-compose up --build  
   ```  
3. Access the API documentation via Swagger at `http://localhost:5000/swagger`.
