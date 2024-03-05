## Packages

1. dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 7.0.0
2. dotnet add package DotNetEnv

## Database

1. Setting database authentication -> **Windows Authentication**
2. In Databases folder, add new database "capstone"

## .env

1. Create .env file
2. Create "DefaultConnection" in .env file
3. Assign the variable with your local connection string SQL Server

**Ex connection string:**
Data Source=_(SERVER)_;Initial Catalog=_(DBNAME)_;Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False

## How to run server

```bash
dotnet run --project WebApi
```
