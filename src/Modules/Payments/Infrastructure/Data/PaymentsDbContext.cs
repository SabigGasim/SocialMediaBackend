using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.BuildingBlocks.Infrastructure.InternalCommands;
using SocialMediaBackend.BuildingBlocks.Infrastructure.Messaging;

namespace SocialMediaBackend.Modules.Payments.Infrastructure.Data;

public class PaymentsDbContext : DbContext
{
    public DbSet<InternalCommand> InternalCommands { get; set; } = default!;
    public DbSet<OutboxMessage> OutboxMessages { get; set; } = default!;
    public DbSet<InboxMessage> InboxMessages { get; set; } = default!;

    protected PaymentsDbContext() { }

    public PaymentsDbContext(DbContextOptions<PaymentsDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema(Schema.Payments);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }
}