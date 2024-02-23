## How to do Migrations ?

- You should be inside the TaskMgmt directory.
- Creating migration file. Here "SeedData" is migration name.
  ```bash
  dotnet ef migrations add SeedData --project TaskMgmt.DataAccess --startup-project TaskMgmt.Api
  ```
- Updating DB with all those migrations.
  ```bash
  dotnet ef database update --project TaskMgmt.DataAccess --startup-project TaskMgmt.Api
  ```
