using ImageGeneratorApi.Core.Image.Interfaces;
using ImageGeneratorApi.Core.Image.Services;
using ImageGeneratorApi.Core.Project.Repository;
using ImageGeneratorApi.Core.Project.Services;
using ImageGeneratorApi.Core.StableDiffusion.Config;
using ImageGeneratorApi.Core.StableDiffusion.Interfaces;
using ImageGeneratorApi.Core.StableDiffusion.Services;
using ImageGeneratorApi.Domain.Images.Repository;
using ImageGeneratorApi.Domain.Images.Services;
using ImageGeneratorApi.Domain.Project.Repository;
using ImageGeneratorApi.Domain.Project.Services;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace ImageGeneratorApi.Core;

public static class ConfigureService
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services)
    {
        StableConfiguration stableConfiguration = new StableConfiguration();
        //TODO: esto deberia ir en infrastructure
        services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddScoped<IProjectRepository, ProjectRepository>();
        services.AddScoped<IProjectService, ProjectService>();
        services.AddScoped<IImageRepository, ImageRepository>();
        services.AddScoped<IProjectImageService, ProjectImageService>();
        services.AddSingleton(stableConfiguration);
        services.AddTransient<IImageGeneratorBannedWords, HardcodedBannedWords>();
        services.AddTransient<IImageGenerator, Automatic1111>();
        services.AddTransient<IImageAnalyser, Automatic1111>();
        services.AddTransient<IImageUpscaler, Automatic1111>();
        services.AddTransient<IImageOutpainter, Automatic1111>();
        services.AddSingleton<StableDiffusionBackendCache>();
        services.AddScoped<IImageGenerationConfigStorage, DatabaseImageGenerationStorage>(); // Changed to scoped
        services.AddSingleton<Func<IApplicationDbContext, IImageGenerationConfigStorage>>(_ =>
            dbContext => new DatabaseImageGenerationStorage(dbContext));
        services.AddSingleton<HttpClient, HttpClient>();
        return services;
    }
}