using ImageGeneratorApi.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Core.Application.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Project> Projects { get; }
    public DbSet<Image> Images { get; }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}