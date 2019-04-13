# DDD-Example-C#
### Includes:
#### Application Layer:
##### ProductHandler:
- Post https://localhost:5001/api/v1/products
- Get https://localhost:5001/api/v1/{id:string} 
- Delete https://localhost:5001/api/v1/{id:string}

#### Application Layer Yet to Add:
- ProductSellsHandler
- MediatR: INotification for DomainEvents
- Explore CQRS

#### Domain Layer:
Work on domain layer is very much finished, might have some refining to do.

#### Infrastructure Layer:
- EFCore
#### Infrastructure Layer Yet to Add:
- Using Dapper (SQL)
- Using DynamoDb (noSQL)

### References
- [DDD-Oriented-Services](https://docs.microsoft.com/en-us/dotnet/standard/microservices-architecture/microservice-ddd-cqrs-patterns/ddd-oriented-microservice)
- [EShopContainer-Ordering-Project](https://github.com/dotnet-architecture/eShopOnContainers)
