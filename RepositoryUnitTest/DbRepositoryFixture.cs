using Database;
using Microsoft.EntityFrameworkCore;

namespace RepositoryUnitTest;

public class DbRepositoryFixture:IDisposable
{
    public GolfRepository GolfRepository;
    private DatabaseContext dbContext;

    public Guid guid = Guid.NewGuid();
        
    public DbRepositoryFixture()
    {
        //Create database for testing
        dbContext = new DatabaseContext();
        dbContext.Database.EnsureDeleted();

        GolfRepository = new GolfRepository();
    }

    public void Dispose()
    {
        dbContext.Dispose();
    }
}