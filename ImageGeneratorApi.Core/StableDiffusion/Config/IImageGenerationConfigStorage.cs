using System.Text.Json.Serialization;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;

namespace ImageGeneratorApi.Core.StableDiffusion.Config;

public interface IImageGenerationConfigStorage
    : IKeyValueStorage<ImageGenerationConfig>;