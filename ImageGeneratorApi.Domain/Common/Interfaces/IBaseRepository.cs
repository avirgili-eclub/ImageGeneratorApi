namespace ImageGeneratorApi.Domain.Common.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    IQueryable<T?> GetAll();
    Task<IReadOnlyList<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    Task CreateRangeAsync(IEnumerable<T> entities, CancellationToken cancellationToken = default);
    Task<T> UpdateAsync(T entity);
    Task DeleteByIdAsync(int id);
    Task SaveChanges();
}