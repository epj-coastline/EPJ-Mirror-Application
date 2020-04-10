# ASP.NET & EFCore Service - Instruction

## Setup Backend

Prerequisites: PostgreSQL, Jetbrains Rider, .NET Core SDK

*  Open solution in Rider and Terminal to restore dependencies run
```c#
dotnet restore
```

* Check if the CLI version is on current version by running
```c#
dotnet ef
```

If errors occur the CLI tools version might be outdated or not installed, to install tools run
```c#
dotnet tool install --global dotnet-ef
```

* In the service project check the appsettings.Development.json if the ConnectionString works with the local postrgeSQL instance
* Start the service project and the database should be automatically migrated
* Run service tests or check the created database via DataGrip / pgAdmin

### Manual Migration

If the auto-migration did not work try migrating the database manually

* Rider user open CLI Console and navigate to DAL-project
* Update database for creating first schema with following command
  
```c#
dotnet ef database update InitialCreate --project ../ServerPrototype.DAL
```

* Run service tests or check the created database via DataGrip / pgAdmin

**References / Documentation**

* EF Core CLI Tools: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
* EF Core Migrations: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

## Test Projects

For testing integration- and unit tests open the Testing folder, hit right-click on test project and run tests.

Testing folder includes

* Service.Testing: Integration tests for service layer

* Repository.Testing: Unit tests for repository layer

## Backend Conventions

### Testing Convention

Following the TDD approach, add more failing tests first, then update the target code. 

**Test naming**

- The name of the method being tested
- The scenario under which it's being tested
- The expected behaviour when the scenario is invoked

Example: `Insert_SingleUser_ReturnsCreatedUser()`

**Arrange, Act, Assert** is a common pattern when unit testing. As the name implies, it consists of three main actions:

- *Arrange* your objects, creating and setting them up as necessary.
- *Act* on an object.
- *Assert* that something is as expected.

**xUnit Framework**

The `[Fact]` attribute declares a test method that's run by the test runner. If there are multiple different input parameters which should result in the same behaviour use the `[Theory]` and `[InlineData(inputparameter)]` attributes to declare a test.

- `[Theory]` represents a suite of tests that execute the same code but have different input arguments.
- `[InlineData]` attribute specifies values for those inputs.

**References / Documentation**

* .NET Unit Testing Best Practices: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-best-practices
* xUnit Framework: https://docs.microsoft.com/en-us/dotnet/core/testing/unit-testing-with-dotnet-test

### Relationship Convention

For migrating the new entities to the database, the CoastlineContext must be updated. For the new entity add an EnitityTypeConfig class and specify the constraints with Fluent-Api including adding seed data to enable testing on the repo layer.

Many-to-many relationships are not supported by EF Core and must be mapped to a new entity to represent the join table.
Naming of join-table entities: `ClassAClassB`

Join-table entities have one primary key combined of both foreign key:
Fluent-Api syntax: `.HasKey(t => new { a.PK, b.PK });`

Do not forget to add the join-table entity as a list reference in the related classes.

References / Documentation

* EF Core relationships: https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key


## Run in Docker 

**Coastline Server and Database**
```
docker-compose up
```

**Build Coastline Server Image**

```
docker build -t coastline-server -f prod.dockerfile .
```

**Build Coastline Server Image**
```
docker run -it --rm -p 80:80 --name aspnetcore_sample coastline-server
```
