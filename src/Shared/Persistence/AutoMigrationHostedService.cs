using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Shared.Persistence;

public class AutoMigrationHostedService(IServiceProvider provider) : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = provider.CreateScope();

        await MigrateAsync(scope.ServiceProvider);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task MigrateAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<AutoMigrationHostedService>>();
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();

        logger.LogInformation("Applying migrations...");
        await dbContext.Database.MigrateAsync();
    }
}