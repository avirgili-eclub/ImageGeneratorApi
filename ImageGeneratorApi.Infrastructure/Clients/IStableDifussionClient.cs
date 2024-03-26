using ImageGeneratorApi.Domain.clients.StableDiffusion.dto;
using Refit;

namespace ImageGeneratorApi.Infrastructure.Clients;

[Headers("accept: application/json")]

public interface IStableDifussionClient
{
        [Get("/sdapi/v1/sd-models")]
    Task<List<StableModel>> GetStableModelsAsync();
}