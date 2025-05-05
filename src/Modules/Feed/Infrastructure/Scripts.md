# Migrations

### Add Migration
dotnet ef migrations add <Migration> --project src/Modules/Feed/Infrastructure --context FeedDbContext --startup-project src/Presentation/Api  --output-dir Data/Migrations/

### Update
dotnet ef database update --project src/Modules/Feed/Infrastructure --context FeedDbContext --startup-project src/Presentation/Api

### Update Specific Migration
dotnet ef database update <Migration> --project src/Modules/Feed/Infrastructure --context FeedDbContext --startup-project src/Presentation/Api  --output-dir Data/Migrations/

### Remove Migrations
dotnet ef database remove --project src/Modules/Feed/Infrastructure --context FeedDbContext --startup-project src/Presentation/Api