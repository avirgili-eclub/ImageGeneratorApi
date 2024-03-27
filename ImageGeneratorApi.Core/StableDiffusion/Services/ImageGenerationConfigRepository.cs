using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;

namespace ImageGeneratorApi.Core.StableDiffusion.Services;

public class ImageGenerationConfigRepository : IImageGenerationConfigRepository
{
    private readonly IApplicationDbContext _dbContext;

    public ImageGenerationConfigRepository(IApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    // Implement methods to access image generation configuration data
}