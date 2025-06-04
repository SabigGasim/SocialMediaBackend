# Migrations

### Add Migration
dotnet ef migrations add <Migration> --project src/Modules/Payments/Infrastructure --context PaymentsDbContext --startup-project src/Database/DatabaseMigrator  --output-dir Data/Migrations/

### Update
dotnet ef database update --project src/Modules/Payments/Infrastructure --context PaymentsDbContext --startup-project src/Database/DatabaseMigrator

### Update Specific Migration
dotnet ef database update <Migration> --project src/Modules/Payments/Infrastructure --context PaymentsDbContext --startup-project src/Database/DatabaseMigrator

### Remove Migrations
dotnet ef migrations remove --project src/Modules/Payments/Infrastructure --context PaymentsDbContext --startup-project src/Database/DatabaseMigrator

### List Migrations
dotnet ef migrations list --project src/Modules/Payments/Infrastructure --context PaymentsDbContext --startup-project src/Database/DatabaseMigrator