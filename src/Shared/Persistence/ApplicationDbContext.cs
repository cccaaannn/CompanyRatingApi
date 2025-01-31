using System.Reflection;
using CompanyRatingApi.Application.Authentication.Entities;
using CompanyRatingApi.Application.Companies.Entities;
using CompanyRatingApi.Shared.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace CompanyRatingApi.Shared.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<AppUser> AppUsers { get; set; }

    public DbSet<Company> Companies { get; set; }

    public DbSet<CompanyRating> CompanyRatings { get; set; }
    
    public DbSet<CompanyComment> CompanyComments { get; set; }

    public override int SaveChanges()
    {
        UpdateAuditFields();
        HandleSoftDelete();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        UpdateAuditFields();
        HandleSoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        FilterSoftDeletedEntities(modelBuilder);
    }

    private void UpdateAuditFields()
    {
        var entries = ChangeTracker.Entries<EntityBase>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                    entry.Entity.CreatedBy = "unknown";
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "unknown";
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                    entry.Entity.UpdatedBy = "unknown";
                    break;
            }
        }
    }

    private void HandleSoftDelete()
    {
        var entries = ChangeTracker.Entries<ISoftDeletable>();

        foreach (var entry in entries)
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Modified;
                    entry.Entity.IsDeleted = true;
                    break;
            }
        }
    }

    private static void SetIsDeletedFilter<T>(ModelBuilder builder) where T : EntityBase, ISoftDeletable
    {
        builder.Entity<T>().HasQueryFilter(e => !e.IsDeleted);
    }

    private static void FilterSoftDeletedEntities(ModelBuilder builder)
    {
        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (
                typeof(EntityBase).IsAssignableFrom(entityType.ClrType)
                &&
                typeof(ISoftDeletable).IsAssignableFrom(entityType.ClrType)
            )
            {
                var method = typeof(ApplicationDbContext)
                    .GetMethod(nameof(SetIsDeletedFilter), BindingFlags.NonPublic | BindingFlags.Static)
                    ?.MakeGenericMethod(entityType.ClrType);

                method?.Invoke(null, [builder]);
            }
        }
    }
}