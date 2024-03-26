namespace ImageGeneratorApi.Domain.Common.Interfaces;

public interface IBaseService<T> where T : BaseEntity
{
    IQueryable<T?> GetAll();
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task<T> UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task SaveChanges();
}