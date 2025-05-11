# Migrations

### Add Migration
dotnet ef migrations add <Migration> --project src/Modules/Users/Infrastructure --context UsersDbContext --startup-project src/Database/DatabaseMigrator  --output-dir Data/Migrations/

### Update
dotnet ef database update --project src/Modules/Users/Infrastructure --context UsersDbContext --startup-project src/Database/DatabaseMigrator

### Update Specific Migration
dotnet ef database update <Migration> --project src/Modules/Users/Infrastructure --context UsersDbContext --startup-project src/Database/DatabaseMigrator  --output-dir Data/Migrations/

### Remove Migrations
dotnet ef migrations remove --project src/Modules/Users/Infrastructure --context UsersDbContext --startup-project src/Database/DatabaseMigrator