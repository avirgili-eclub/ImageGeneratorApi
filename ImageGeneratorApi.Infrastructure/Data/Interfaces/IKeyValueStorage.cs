namespace ImageGeneratorApi.Infrastructure.Data.Interfaces;

public interface IKeyValueStorage<TValue>
    where TValue : class
{
    public Task<TValue?> Get(ulong id);

    public Task Put(ulong id, TValue data);

    public Task<bool> Delete(ulong id);

    public Task<int> Count();

    public Task<TValue?> Random();
}