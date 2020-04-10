# ASP.NET & EFCore Service - Instruction

## Setup Backend

Prerequisites: PostgreSQL, Jetbrains Rider, .NET Core SDK

*  Open solution in Rider and Terminal to restore dependencies run:
```c#
dotnet restore
```

* Check if the CLI version is on current version by running:
```c#
dotnet ef
```

If errors occur the CLI tools version might be outdated or not installed, to install tools run:
```c#
dotnet tool install --global dotnet-ef
```

* In the service project check the appsettings.Development.json if the ConnectionString works with the local postrgeSQL instance
* Start the service project and the database should be automatically migrated
* Run service tests or check the created database via DataGrip / pgAdmin

### Manual Migration

If the auto-migration did not work try migrating the database manually

* Rider user open CLI Console and navigate to DAL-project
* Update database for creating first schema with following command:
  
```c#
dotnet ef database update InitialCreate --project ../ServerPrototype.DAL
```

* Run service tests or check the created database via DataGrip / pgAdmin

**References / Documentation:**

* EF Core CLI Tools: https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet
* EF Core Migrations: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli

## Test Projects

For testing integration- and unit tests open the Testing folder, hit right-click on test project and run tests.

Testing folder includes:

* Service.Testing: Integration tests

* Repository.Testing: Unit tests

### Testing Conventions




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
