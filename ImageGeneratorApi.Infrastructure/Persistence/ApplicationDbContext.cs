using System.Security.Claims;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Common.Entities;
using ImageGeneratorApi.Domain.Images.Entities;
using ImageGeneratorApi.Domain.Project.Entities;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace ImageGeneratorApi.Infrastructure.Persistence;

using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : IdentityDbContext<User>, IApplicationDbContext
{
    private readonly IDateTime _dateTime;
    private readonly ICurrentUserService _currentUserService;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options,
        IDateTime dateTime,
        //TODO: is deprecated
        IHttpContextAccessor httpContextAccessor, ICurrentUserService currentUserService) : base(options)
    {
        _dateTime = dateTime;
        _currentUserService = currentUserService;
    }

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

    public DbSet<Project> Projects { get; set; } 
    public DbSet<Image> Images { get; set; }

    public DbSet<T> Set<T>() where T : class
    {
        return Set<T>();
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        //TODO: Fix this.
        
        foreach (var entry in ChangeTracker.Entries<BaseEntity>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedBy = _currentUserService?.UserName;
                    entry.Entity.CreatedAt = _dateTime.Now;
                    entry.Entity.Id = Guid.NewGuid();
                    break;
        
                case EntityState.Modified:
                    entry.Entity.UpdatedBy = _currentUserService?.UserName;
                    entry.Entity.UpdatedAt = _dateTime.Now;
                    // No modificar el ID
                    entry.Property(x => x.Id).IsModified = false;
                    break;
                
                case EntityState.Deleted:
                    entry.Entity.DeletedBy = _currentUserService?.UserName;
                    entry.Entity.DeletedAt = DateTime.Now;
                    entry.Entity.IsDeleted = true;
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