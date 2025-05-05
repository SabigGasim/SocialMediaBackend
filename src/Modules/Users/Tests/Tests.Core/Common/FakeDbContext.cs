using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Modules.Users.Infrastructure.Data;
using SocialMediaBackend.Modules.Users.Tests.Core.Common.Users;

namespace SocialMediaBackend.Modules.Users.Tests.Core.Common;

public class FakeDbContext : UsersDbContext
{
    public FakeDbContext()
    {

    }

    public FakeDbContext(DbContextOptions<UsersDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<FakeUserResource>()
            .HasKey(x => x.Id);

        modelBuilder.Entity<FakeUserResource>()
            .ToTable("FakeUserResources");

        modelBuilder.Entity<FakeUserResource>()
            .Property(x => x.Id)
            .HasConversion(
                id => id.Value,
                value => new(value)
            );

        modelBuilder.Entity<FakeUserResource>()
            .HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        base.OnModelCreating(modelBuilder);
    }
}