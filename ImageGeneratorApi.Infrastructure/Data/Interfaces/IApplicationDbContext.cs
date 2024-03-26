using ImageGeneratorApi.Domain.Common.Entities;
using ImageGeneratorApi.Domain.Images.Entities;
using ImageGeneratorApi.Domain.Project.Entities;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Infrastructure.Data.Interfaces;

public interface IApplicationDbContext
{
    public DbSet<User> Users { get; }
    public DbSet<Project> Projects { get; set; }
    public DbSet<Image> Images { get; set; }
    
    DbSet<T> Set<T>() where T : class;


    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}