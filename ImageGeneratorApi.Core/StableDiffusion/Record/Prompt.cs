namespace ImageGeneratorApi.Core.StableDiffusion.Record;

public record Prompt
{
    public required string Positive { get; set; }
    public required string Negative { get; set; }

    public string? FaceEnhancementPositive { get; set; }
    public string? FaceEnhancementNegative { get; set; }

    public string? EyeEnhancementPositive { get; set; }
    public string? EyeEnhancementNegative { get; set; }

    public string? HandEnhancementPositive { get; set; }
    public string? HandEnhancementNegative { get; set; }
}