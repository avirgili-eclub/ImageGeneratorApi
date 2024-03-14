using ImageGeneratorApi.Domain.Entities;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ImageGeneratorApi.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(
    DbContextOptions<ApplicationDbContext> options,
    IDateTime dateTime,
    //TODO: is deprecated
    IHttpContextAccessor _httpContextAccessor)
    : IdentityDbContext<User>(options), IApplicationDbContext
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        
        //TODO: Mover a un archivo de configuración aparte.
        modelBuilder.Entity<Project>()
            .HasOne(p => p.User)             // Each project has one user
            .WithMany(u => u.Projects)       // Each user can have many projects
            .HasForeignKey(p => p.UserId);  // Foreign key in Project pointing to UserId in User
        
        base.OnModelCreating(modelBuilder);
    }

    public DbSet<Project> Projects => Set<Project>();
    public DbSet<Image> Images => Set<Image>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //TODO: Fix this.
        // var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        //
        // foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        // {
        //     switch (entry.State)
        //     {
        //         case EntityState.Added:
        //             entry.Entity.CreatedBy = userId;
        //             entry.Entity.CreatedAt = dateTime.Now;
        //             entry.Entity.Id = Guid.NewGuid();
        //             break;
        //
        //         case EntityState.Modified:
        //             entry.Entity.UpdatedBy = userId;
        //             entry.Entity.UpdatedAt = dateTime.Now;
        //             // No modificar el ID
        //             entry.Property(x => x.Id).IsModified = false;
        //             break;
        //     }
        // }

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