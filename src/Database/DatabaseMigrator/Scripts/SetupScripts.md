# Migrations

### Migrate all schemas
dotnet ef database update --project src/Modules/Users/Infrastructure --context UsersDbContext --startup-project src/Database/DatabaseMigrator
dotnet ef database update --project src/Modules/Feed/Infrastructure --context FeedDbContext --startup-project src/Database/DatabaseMigrator
dotnet ef database update --project src/Modules/Chat/Infrastructure --context ChatDbContext --startup-project src/Database/DatabaseMigrator
dotnet ef database update --project src/Modules/AppSubscriptions/Infrastructure --context SubscriptionsDbContext --startup-project src/Database/DatabaseMigrator