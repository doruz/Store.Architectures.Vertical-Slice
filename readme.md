# Onion Architecture in .NET

## Application

### Requirements
Designing a simple Store application with following use cases:
* **Admin**
  * Managing product details: adding, editing and deleting them.
* **Customer**
  * Creating new orders by adding or removing available products, updating products quantities.
  * Checking orders history.

### Technologies stack
In order to keep things simpler and cleaner, the tools used to build this solution was kept to the minimum:
* [ASP.NET Core](https://dotnet.microsoft.com/en-us/apps/aspnet)
  * [Swagger](https://swagger.io/)
  * [Postman](https://www.postman.com/)
* [Azure Cosmos DB](https://learn.microsoft.com/en-us/azure/cosmos-db/)
  * Locally could be used [Cosmos DB emulator](https://learn.microsoft.com/en-us/azure/cosmos-db/how-to-develop-emulator?pivots=api-nosql&tabs=windows%2Ccsharp)
* Testing
  * Unit & Integration Tests:
    * [xUnit](https://github.com/xunit/)
    * [Fluent Assertions](https://fluentassertions.com/)
  * Architecture Tests
    * [xUnit](https://github.com/xunit/)
    * [Fluent Assertions](https://fluentassertions.com/)
    * [NetArchTest](https://github.com/BenMorris/NetArchTest)

## Solution design

### Layers

 (Onion | Clean) Architecture aims to separate the concerns of the application into distinct layers, promoting high cohesion and low coupling. Current solution consists of the following layers: 

- **Core**: core business logic of the application
  - **Shared**: contains common logic (extensions, utilities, etc.) which could be shared on all projects from the solution.
  - **Domain**: contains business objects: entities, entities logic, value objects, interfaces.
  - **Business**: contains business use cases, business services interfaces (file storage, notifications, etc.).
  
- **Infrastructure**: implementation of external dependencies like databases, file storage, notifications and so on.
  -  **Persistence**: persistence implementation using databases (NoSQL DB, SQL DB, distributed caching and others).
  
-  **Presentation**: exposes application to the outside world through REST, Web App, gRPC and others. 
   -  **Api**: exposes business use cases as REST endpoints.

### Solution folders

These is the structure of the application using Visual Studio virtual folders.
```
Store
├───src
│   ├───Core
│   │   ├───Store.Core.Business
│   │   ├───Store.core.Domain
│   │   └───Store.Core.Shared
│   ├───Infrastructure
│   │   └───Store.Infrastructure.Persistence   
│   └───Store.Presentation.Api
└───tests
    ├───Core
    │   ├───Store.Core.Domain.Tests
    │   └───Store.Core.Shared.Tests
    ├───Store.Presentation.Api.IntegrationTests
    └───Store.Tests
```

### Solution dependencies

Next diagram shows how dependencies look like between each layer of the application.
This is extracted using Visual Studio and represents the current implementation.  

![Projects dependencies diagram](https://raw.githubusercontent.com/doruz/Store.Architectures.Onion/refs/heads/main/Docs/store_solution_architecture.png)

### Solution rules
While designing the solution there were implemented a few fitness functions, using [NetArchTest](https://github.com/BenMorris/NetArchTest), to make sure that design conventions are not broken:
* Solution dependencies from diagram above are not broken.
* Domain value types and business models are immutable types.
* Business services and models are used by *Presentation* to expose application use cases. We have to make sure that *Business* is not exposing domain objects to the outside world.
* *Presentation* is not directly using types from *Infrastructure*.
* Types from *Infrastructure* are not exposed outside their projects. These could be used only by using DI and abstractions types from *Core* & *Business*.
* Naming conventions for types and namespaces.

