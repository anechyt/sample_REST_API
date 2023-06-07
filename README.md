# sample_REST_API
In this soulution I create a REST API using C# and MS SQL database.

SampleRestApi.Domain : in this directory we have our entities.

SampleRestApi.Persistence : This class library implements a level for working with the database, as well as configuration and migration

SampleRestApi.Application : The Application Layer acts as a bridge between the Presentation Layer and the Domain Layer. It contains application-specific logic and manages the execution of use cases or application workflows. This layer validates inputs, applies business rules, and interacts with the Domain Layer.

UI : in this folder we have our API with DogController :
- method Ping https://localhost:5001/api/ping ( return messege "Dogs house service. Version 1.0.1" )
- method dogs https://localhost:5001/api/dogs ( return list of dogs )
- method dog https://localhost:5001/api/dog ( create a dog )

Both sorting and pagination work together.
If no data is entered for sorting and/or pagination the API will return all records from the database

SampleRestApi.Tests :  In this class libraryr we create our test.
