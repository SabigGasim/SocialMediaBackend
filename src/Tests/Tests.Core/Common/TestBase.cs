using Microsoft.EntityFrameworkCore;
using SocialMediaBackend.Infrastructure.Data;

namespace Tests.Core.Common;

public abstract class TestBase : IDisposable
{
    protected readonly FakeDbContext _dbContext;

    public TestBase()
    {
        _dbContext = CreateInMemoryDbContext();
    }

    private static FakeDbContext CreateInMemoryDbContext()
    {
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new FakeDbContext(options);
    }

    public void Dispose()
    {
        _dbContext.Database.EnsureDeleted();
        _dbContext.Dispose();
        GC.SuppressFinalize(this);
    }
}
