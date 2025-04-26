using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Infrastructure.Data;

namespace Tests.Core.Common;

public class FakeDbContext : ApplicationDbContext
{
    public FakeDbContext()
    {

    }

    public FakeDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<FakeUserResource>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<FakeUserResource>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        modelBuilder.Entity<FakeUserResource>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId);
    }
}