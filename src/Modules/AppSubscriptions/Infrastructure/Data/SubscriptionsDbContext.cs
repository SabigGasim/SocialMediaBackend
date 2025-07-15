using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.AppPlan;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Authorization;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.SubscriptionPayments;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Subscriptions;
using SocialMediaBackend.Modules.AppSubscriptions.Domain.Users;

namespace SocialMediaBackend.Modules.AppSubscriptions.Infrastructure.Data;

public sealed class SubscriptionsDbContext : DbContext
{
    public DbSet<User> Users { get; private set; } = default!;
    public DbSet<AppSubscriptionProduct> AppSubscriptionProducts { get; private set; } = default!;
    public DbSet<Subscription> Subscriptions { get; private set; } = default!;
    public DbSet<SubscriptionPayment> SubscriptionPayments { get; private set; } = default!;

    public DbSet<Role> Roles { get; private set; } = default!;
    public DbSet<InternalCommand> InternalCommands { get; private set; } = default!;
    public DbSet<InboxMessage> InboxMessages { get; private set; } = default!;
    public DbSet<OutboxMessage> OutboxMessages { get; private set; } = default!;

    public SubscriptionsDbContext(DbContextOptions<SubscriptionsDbContext> options) : base(options) { }
    private SubscriptionsDbContext() { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.AppSubscriptions);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}
