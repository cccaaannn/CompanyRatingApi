using CompanyRateApi.Shared.Attributes.Injectable;
using Microsoft.EntityFrameworkCore;

namespace CompanyRateApi.Shared.Persistence;

[Injectable]
public class DbContextBootstrap(ILogger<DbContextBootstrap> logger, ApplicationDbContext dbContext)
{
    public async Task InitializeAsync()
    {
        logger.LogInformation("Applying migrations...");
        await dbContext.Database.MigrateAsync();
    }
}
