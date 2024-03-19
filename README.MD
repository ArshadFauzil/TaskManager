# Task Manager .NET Core Service

A .NET Core Web API service for managing everyday tasks.

---

**IMPORTANT NOTE**

This project requires the .NET SDK and CLI preinstalled. Available SDKs can be found here [.NET SDK Downloads](https://dotnet.microsoft.com/en-us/download).

This project uses a SQLite database which is set up by Code-First Migration using Entity Framework Core. The EF CLI can be installed as follows.

#### `dotnet tool install --global dotnet-ef`

This project has a React frontend app that can be found here - [Task Manager GUI](https://github.com/ArshadFauzil/TaskManager-UI).

---

## Setup Instructions

Since this project uses EF Core, the database must be set up first. In the project directory, run:

#### `dotnet ef migrations add <nameOfMigrationOfYourChoice>  --project TaskManager`

This will create all the migration files necessary to create/update the database schema.

Then run:

#### `dotnet ef database update --project TaskManager`

This will create the database and apply all the schema changes specified in the migration folder.

Next run:

#### `dotnet build`

To check any if build errors were found.

Finally run:

#### `dotnet run --project TaskManager`

Your Task Manager Web API service is now up and running.

Documentation related to the APIs defined in this project can be found in the `Docs` folder.
