using System.Text.Json.Serialization;

namespace ImageGeneratorApi.Domain.clients.StableDiffusion.dto;

public class StableModel
{
    public string Title { get; set; }
    [JsonPropertyName("model_name")]
    public string ModelName { get; set; }
    public string Hash { get; set; }
    public string Sha256 { get; set; }
    public string Filename { get; set; }
    public string Config { get; set; }
}