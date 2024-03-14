using ImageGeneratorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Infrastructure.Data.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Domain.Entities.Project> Projects { get; }
    public DbSet<Image> Images { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}