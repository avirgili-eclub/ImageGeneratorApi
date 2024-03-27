using ImageGeneratorApi.Core.StableDiffusion.Config;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Persistence;

namespace ImageGeneratorApi.Core.StableDiffusion.Services;

public class DatabaseImageGenerationStorage(IApplicationDbContext database)
    : SimpleJsonBlobTable<ImageGenerationConfig>("ImageGeneration", database), IImageGenerationConfigStorage;