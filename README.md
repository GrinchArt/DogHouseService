# DogHouseService

DogHouseService is a REST API for managing dog information. The project is implemented using .NET Core with CQRS and Mediator design patterns, following Clean Architecture principles. The API supports CRUD operations, sorting, pagination, and request rate limiting.


#Table of Contents

Features
Architecture



#Features

Ping Endpoint: Check the service's availability.
CRUD Operations for Dogs: Create, read.
Sorting and Pagination Support: Queries can be sorted by various attributes and support pagination.
Rate Limiting: Protection against excessive requests using the Token Bucket algorithm.
Clean Architecture: The project follows Clean Architecture principles with layer separation.
Tests: Includes unit tests for handlers, mappers, and middleware.

#Architecture
The project is implemented according to Clean Architecture principles:

DogHouseService.Domain: Contains domain entities and interfaces.
DogHouseService.Application: Implements business logic using CQRS and Mediator patterns. Contains command and query handlers, DTOs, and validators.
DogHouseService.Infrastructure: Includes repository implementations, middleware, and database configuration.
DogHouseService.API: Contains controllers and service configuration for Dependency Injection.
DogHouseService.Tests: Includes unit tests for business logic and middleware.
