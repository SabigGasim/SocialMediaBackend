using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Domain.Comments;
using SocialMediaBackend.Domain.Common;
using SocialMediaBackend.Domain.Posts;
using SocialMediaBackend.Domain.Users;
using SocialMediaBackend.Domain.Users.Follows;
using SocialMediaBackend.Infrastructure.Processing;

namespace SocialMediaBackend.Infrastructure.Data;
public class ApplicationDbContext : DbContext, IUnitOfWork
{
    private readonly IDomainEventsDispatcher _dispatcher;

    public DbSet<User> Users { get; set; }
    public DbSet<Post> Posts { get; set; }  
    public DbSet<Comment> Comments { get; set; }
    public DbSet<Follow> Follows { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDomainEventsDispatcher dispatcher) : base(options) 
    {
        _dispatcher = dispatcher;
    }

    protected ApplicationDbContext() {}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(IInfrastructureMarker).Assembly);
    }

    async Task<int> IUnitOfWork.CommitAsync(CancellationToken ct)
    {
        var entitiesWithEvents = ChangeTracker.Entries<IHasDomainEvents>()
           .Select(e => e.Entity)
           .Where(e => e.DomainEvents?.Count > 0)
           .ToList();

        var events = entitiesWithEvents
            .SelectMany(e => e.DomainEvents!)
            .ToList();

        foreach (var entity in entitiesWithEvents)
        {
            entity.ClearDomainEvents();
        }

        await _dispatcher.DispatchAsync(events, ct);

        return await base.SaveChangesAsync(ct); ;
    }
}

