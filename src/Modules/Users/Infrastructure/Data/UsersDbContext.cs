﻿using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.Users.Domain.Authorization;
using SocialMediaBackend.Modules.Users.Domain.Users;
using SocialMediaBackend.Modules.Users.Domain.Users.Follows;

namespace SocialMediaBackend.Modules.Users.Infrastructure.Data;

public class UsersDbContext : DbContext
{
    public DbSet<User> Users { get; set; } = default!;
    public DbSet<Follow> Follows { get; set; } = default!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; }
    public DbSet<Role> Roles { get; set; } = default!;

    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected UsersDbContext() {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Users);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}

