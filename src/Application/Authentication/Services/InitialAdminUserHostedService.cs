using CompanyRatingApi.Application.Authentication.Configurations;
using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Shared.Auth;
using CompanyRatingApi.Shared.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Application.Authentication.Services;

public class InitialAdminUserHostedService(IServiceProvider provider): IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        using var scope = provider.CreateScope();

        await SeedAdminUserAsync(scope.ServiceProvider);
    }

    public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;

    private async Task SeedAdminUserAsync(IServiceProvider serviceProvider)
    {
        var logger = serviceProvider.GetRequiredService<ILogger<InitialAdminUserHostedService>>();
        var adminUserConfig = serviceProvider.GetRequiredService<AdminUserConfig>();
        var dbContext = serviceProvider.GetRequiredService<ApplicationDbContext>();
        var hashUtils = serviceProvider.GetRequiredService<IHashUtils>();

        var adminUser = await dbContext.AppUsers.FirstOrDefaultAsync(u => u.Id == adminUserConfig.Id);

        if (adminUser == null)
        {
            logger.LogInformation("Seeding admin user...");

            var hashedPassword = hashUtils.Hash(adminUserConfig.Password);

            adminUser = new AppUser()
            {
                Id = adminUserConfig.Id,
                Email = adminUserConfig.Email,
                Password = hashedPassword,
                Name = adminUserConfig.Name,
                Surname = adminUserConfig.Surname,
                Role = UserRole.Admin
            };

            dbContext.AppUsers.Add(adminUser);
            await dbContext.SaveChangesAsync();
        }
    }
}