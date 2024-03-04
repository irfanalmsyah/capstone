## Packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 7.0.0
dotnet add package DotNetEnv

## Database
Create capstone database

## .env
1. Create .env file
2. Create "DefaultConnection" in .env file
2. Assign the variable with your local connection string SQL Server

### Ex connection string: Data Source=(SERVER);Initial Catalog=(DBNAME);Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False

## How to run server
1. dotnet dev-certs https --trust
2. dotnet run --launch-profile https