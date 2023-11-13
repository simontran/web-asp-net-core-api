# web-asp-net-core-api
WebAPI - ASP.Net Core
-----------------------------------------------
A- Onion Architecture

1- Layers in Onion Architecture
   Onion architecture uses the concept of the layer but is different from N-layer architecture and 3-Tier architecture.

2- Domain Layer
   This layer lies in the center of the architecture where we have application entities which are the application model classes or database model classes.
   Using the code first approach in the application development using Asp.net core these entities are used to create the tables in the database.

3- Repository Layer
   The repository layer act as a middle layer between the service layer and model objects.
   We will maintain all the database migrations and database context Objects in this layer.
   We will add the interfaces that consist of the data access pattern for reading and writing operations with the database.

4- Service Layer
   This layer is used to communicate with the presentation and repository layer.
   The service layer holds all the business logic of the entity.
   In this layer services interfaces are kept separate from their implementation for loose coupling and separation of concerns.

5- Presentation Layer
   In the case of the API Presentation layer that presents us the object data from the database using the HTTP request in the form of JSON Object.
   But in the case of front-end applications, we present the data using the UI by consuming the APIS.

B- Advantages of Onion Architecture
   - Onion architecture provides us with the batter maintainability of code because code depends on layers.
   - It provides us with better testability for unit tests, we can write the separate test cases in layers without affecting the other module in the application.
   - Using the onion architecture our application is loosely coupled because our layers communicate with each other using the interface.
   - Domain entities are the core and center of the architecture and have access to databases and UI Layer.
   - A complete implementation would be provided to the application at run time.
   - The external layer never depends on the external layer.

https://www.c-sharpcorner.com/article/onion-architecture-in-asp-net-core-6-web-api/
