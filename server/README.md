# Coastline Server

*For all commands make sure you are in the `server` directory, not in the repository root.*

## Project setup

**Prerequisites**

- Jetbrains Rider
- [.NET Core 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1)
- [PostgresSQL](https://www.postgresql.org/) or [Docker](https://www.docker.com/)

**Install dependencies**

Open the `CoastlineServer.sln` in Rider and restore all dependencies.

```c#
dotnet restore
```

**Database**

- If you run the database in Docker  you can skip this step.

- Go to the `appsettings.Development.json` in the `CoastlineServer.Service` project. Make sure that `ConnectionStringCoastline` with your local PostgreSQL installation works.

## Run Coastline Server

### Local PostgreSQL installation

- Start the `CoastlineServer.Service` run configuration.
- The database should be automatically migrated.
- Run integration tests or check the created database via DataGrip / pgAdmin.

### PostgresSQL in Docker

**Start a docker container with PostgreSQL**

```
docker run --name coastline-db -p 7000:5432 -e POSTGRES_PASSWORD=mysecretpassword -d postgres
```

- Start the `CoastlineServer.Service Docker DB` run configuration.
- The database should be automatically migrated.
- Run integration tests or check the created database via DataGrip / pgAdmin.

**Integration test**

If the database runs in a docker container one has to set the connection string manually.

```
ConnectionStringCoastline="Server=127.0.0.1;Port=7000;Database=coastline;User Id=postgres;Password=mysecretpassword;" dotnet test
```

## Manual migration

**Entity Framework Core .NET CLI Tools**

Check if the Entity Framework Core .NET CLI Tools are installed and on current version.

```c#
dotnet ef
```

If errors occur the CLI tools version might be outdated or not installed. To install tools run:
```c#
dotnet tool install --global dotnet-ef
```

Navigate to the `CoastlineServer.Service` and Update the database for creating first schema.

```c#
cd CoastlineServer.Service 
dotnet ef database update InitialCreate --project ../CoastlineServer.DAL
```

Run integration tests or check the created database via DataGrip / pgAdmin.

**References / Documentation**

* [EF Core CLI Tools]( https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet)
* [EF Core Migrations]( https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli)

## Create migration

```
 dotnet ef migrations add InitialCreate --project ../CoastlineServer.DAL/
```

## Test Projects

- For integration and unit tests open the `Testing` folder, hit right-click on a test project and run tests.

- Testing folder includes:
  - `Service.Testing`: Integration tests for service layer

  - `Repository.Testing`: Unit tests for repository layer

## Conventions

### Testing Convention

Following the TDD approach, add more failing tests first, then update the target code. 

**Test naming**

- The name of the method being tested
- The scenario under which it's being tested
- The expected behaviour when the scenario is invoked

Example: `Insert_SingleUser_ReturnsCreatedUser()`

**Arrange, Act, Assert** is a common pattern for unit testing. As the name implies, it consists of three main actions:

- *Arrange* your objects, creating and setting them up as necessary.
- *Act* on an object.
- *Assert* that something is as expected.

**xUnit Framework**

The `[Fact]` attribute declares a test method that runs by the test runner. If there are multiple different input parameters which should result in the same behaviour use the `[Theory]` and `[InlineData(inputparameter)]` attributes to declare a test.

- `[Theory]` represents a suite of tests that execute the same code but have different input arguments.
- `[InlineData]` attribute specifies values for those inputs.

**References / Documentation**

* [.NET Unit Testing Best Practices]( https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices)
* [xUnit Framework]( https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test)

### Relationship Convention

- For migrating the new entities to the database, the CoastlineContext must be updated. For the new entity add an EnitityTypeConfig class and specify the constraints with Fluent-API including adding seed data to enable testing on the repo layer.
- Many-to-many relationships are not supported by EF Core.
  - They must be mapped to a new entity to represent the join table.
  - Naming of join-table entities: `ClassAClassB`
  - Join-table entities have one primary key combined of both foreign key.
  - Fluent-Api syntax: `.HasKey(t => new { a.PK, b.PK });`
  - Do not forget to add the join-table entity as a list reference in the related classes.

**References / Documentation**

* [EF Core relationships](https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key)

## Swagger API documentation

**Swagger UI**: https://api.coastline.app/

By adding the `[ApiConventionType(typeof(DefaultApiConventions))]` annotation on each controller class Swagger learns about the possible responses. This annotation adds the responses according to the [default API convention](https://docs.microsoft.com/en-us/dotnet/api/microsoft.aspnetcore.mvc.defaultapiconventions?view=aspnetcore-3.1). Therefore the implementation should stick to this convnetion. 

**References / Documentation**

- [Get started with NSwag and ASP.NET Core](https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-nswag?view=aspnetcore-3.1&tabs=visual-studio)

- [Use web API conventions](https://docs.microsoft.com/en-us/aspnet/core/web-api/advanced/conventions?view=aspnetcore-3.1)

## Run Coastline Server with Docker 

**Run Coastline Server and Database**

```
docker-compose up --build
```

Coastline Server listens on port 5000.

**Remove containers and network**

```
docker-compose down
```

**Build Coastline Server Image**

```
docker build -t coastline-server -f prod.dockerfile .
```
