# ASP.NET & EFCore Service - Instruction

## Setup ServerPrototype

Prerequisites: PostgreSQL, C# IDE

* [ ] Open solution in IDE
* [ ] Dependencies are loaded automatically if not rebuild solution (Rider-user download Nuget Package Microsoft.EntityFrameworkCore.Tools.DotNet in service project)
* [ ] Startup project: ServerPrototype.Service (already default but check if non set)
* [ ] Update ConnectionString in Context-Class with own username & password
* [ ] Tables are created by inserting data from migration to database
*  VS user open Package Manager Console and set DAL-project as default
* Update database for creating first schema with following command:
  
```c#
Update-Database -Migration InitialCreate
```

* Rider user open CLI Console and navigate to DAL-project
* Update database for creating first schema with following command:
  
```c#
dotnet ef database update InitialCreate --project ../ServerPrototype.DAL
```


* Insert seed data by running statements in DataGrip/pgAdmin or run file with psql. View data in pgAdmin/DataGrip

## Test Projects

For testing open the Testing folder, hit right-click on test project and run tests.





