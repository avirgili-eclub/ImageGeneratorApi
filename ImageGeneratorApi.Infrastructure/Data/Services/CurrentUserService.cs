using System.Security.Claims;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using Microsoft.AspNetCore.Http;

namespace ImageGeneratorApi.Infrastructure.Data.Services;

public class CurrentUserService : ICurrentUserService
{
    
    private readonly IHttpContextAccessor _httpContextAccessor;
    
    
    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        this._httpContextAccessor = httpContextAccessor;
    }
    
    public string? UserName => _httpContextAccessor.HttpContext?.User?.Identity?.Name;

    public string? UserId => _httpContextAccessor.HttpContext?.User?.Claims?
        .FirstOrDefault(c => c.Type == System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
}