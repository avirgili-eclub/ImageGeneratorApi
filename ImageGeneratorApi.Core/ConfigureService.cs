using ImageGeneratorApi.Core.Image.Interfaces;
using ImageGeneratorApi.Core.Image.Services;
using ImageGeneratorApi.Core.Project.Repository;
using ImageGeneratorApi.Core.Project.Services;
using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Domain.Services;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGeneratorApi.Core;

public static class ConfigureService
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        //TODO: esto deberia ir en infrastructure
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();
        
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IProjectRepository, ProjectRepository>(); 
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IImageService, ImageService>();
        return services;
    }
}