using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace ImageGeneratorApi.Infrastructure.Data.Services;

public class BaseService<T> : IBaseService<T> where T : BaseEntity
{
    private readonly IBaseRepository<T> _repository;

    public BaseService(IBaseRepository<T> repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public IQueryable<T?> GetAll()
    {
        return _repository.GetAll();
    }

    public async Task<IReadOnlyList<T>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<T?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<T> CreateAsync(T entity)
    {
        return await _repository.CreateAsync(entity);
    }

    public async Task<T> UpdateAsync(T entity)
    {
        return await _repository.UpdateAsync(entity);
    }

    public async Task DeleteByIdAsync(int id)
    {
        await _repository.DeleteByIdAsync(id);
    }

    public async Task SaveChanges()
    {
        await _repository.SaveChanges();
    }
}