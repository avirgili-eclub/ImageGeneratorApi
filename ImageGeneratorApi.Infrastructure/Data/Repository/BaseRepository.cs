using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Infrastructure.Data.Repository;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    protected readonly IApplicationDbContext Context;

    public BaseRepository(IApplicationDbContext context)
    {
        Context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IQueryable<T> GetAll()
    {
        return Context.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await Context.Set<T>().ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id)
    {
        var entity = await Context.Set<T>().FindAsync(id);
        if (entity != null) return entity;
        
        //TODO: throw new EntityNotFoundException($"Entity with ID {id} not found.");
        Console.Error.WriteLine($"Entity with ID {id} not found.");
        throw new Exception($"Entity with ID {id} not found.");
    }

    public async Task<T> CreateAsync(T entity)
    {
        await Context.Set<T>().AddAsync(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default)
    {
        await Context.Set<T>().AddRangeAsync(entities, cancellationToken);
        await Context.SaveChangesAsync(cancellationToken);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        Context.Set<T>().Update(entity);
        await Context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        Context.Set<T>().Remove(entity);
        await Context.SaveChangesAsync();
    }

    public async Task SaveChanges()
    {
        await Context.SaveChangesAsync();
    }
}