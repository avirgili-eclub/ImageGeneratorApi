using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Infrastructure.Data.Services;

public class BaseService<T> : IBaseService<T> where T : class
{
    private readonly ApplicationDbContext _applicationDbContext;

    public BaseService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext ?? throw new ArgumentNullException(nameof(applicationDbContext));
    }

    public IQueryable<T?> GetAll()
    {
        return _applicationDbContext.Set<T>();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _applicationDbContext.Set<T>().ToListAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _applicationDbContext.Set<T>().FindAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        _applicationDbContext.Set<T>().Add(entity);
        await _applicationDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _applicationDbContext.Set<T>().Update(entity);
        await _applicationDbContext.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteByIdAsync(int id)
    {
        var entity = await _applicationDbContext.Set<T>().FindAsync(id);
        if (entity != null)
        {
            _applicationDbContext.Set<T>().Remove(entity);
            await _applicationDbContext.SaveChangesAsync();
        }
    }

    public async Task SaveChanges()
    {
        await _applicationDbContext.SaveChangesAsync();
    }
}