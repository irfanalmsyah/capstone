## Packages
dotnet add package Microsoft.EntityFrameworkCore.SqlServer -v 7.0.0
dotnet add package DotNetEnv

## Database
Create capstone database

## .env
1. Create DefaultConnection variable
2. Assign with your local connection string db 

Ex connection string: Data Source=(SERVER);Initial Catalog=(DBNAME);Integrated Security=True;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False