namespace ImageGeneratorApi.Infrastructure.Data.Interfaces;

public interface ICurrentUserService
{
    public string? UserId { get; }
    public string? UserName { get; }
}