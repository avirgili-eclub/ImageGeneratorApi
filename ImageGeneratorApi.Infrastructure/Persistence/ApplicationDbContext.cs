using ImageGeneratorApi.Core.Interfaces;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDateTime dateTime)
    : DbContext(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<User> Users => Set<User>();
    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Image> Images => Set<Image>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = "UserId";
                    entry.Entity.CreatedAt = dateTime.Now;
                    entry.Entity.Id = Guid.NewGuid();
                    break;

                case EntityState.Modified:
                    entry.Entity.UpdatedBy = "UserId";
                    entry.Entity.UpdatedAt = dateTime.Now;
                    entry.Entity.Id = Guid.NewGuid();
                    break;
            }
        }

        var result = 0;

        try
        {
            result = await base.SaveChangesAsync(cancellationToken);
        }
        catch (DbUpdateConcurrencyException ex)
        {
            // Update the values of the entity that failed to save from the store (https://docs.microsoft.com/es-es/ef/ef6/saving/concurrency)
            ex.Entries.Single().Reload();
        }

        return result;
    }
}