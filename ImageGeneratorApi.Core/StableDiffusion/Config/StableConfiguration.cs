using JetBrains.Annotations;

namespace ImageGeneratorApi.Core.StableDiffusion.Config;

public class StableConfiguration
{
    // [UsedImplicitly] public AuthConfig? Auth;
    [UsedImplicitly] public Automatic1111Config? Automatic1111;
    [UsedImplicitly] public GlobalImageGenerationConfig? ImageGeneration;

    [UsedImplicitly] public bool ProcessMessagesFromSelf;
    [UsedImplicitly] public char PrefixCharacter = '!';
}

// public class AuthConfig
// {
//     [UsedImplicitly] public string? Token;
//     [UsedImplicitly] public string? ClientId;
// }

public class Automatic1111Config
{
    [UsedImplicitly] public Backend[] Backends = null!;

    [UsedImplicitly] public string? Text2ImageSampler = null;
    [UsedImplicitly] public string? Image2ImageSampler = null;
    [UsedImplicitly] public int? SamplerSteps = null;
    [UsedImplicitly] public int? OutpaintSteps = null;
    [UsedImplicitly] public string? Checkpoint = null;
    [UsedImplicitly] public uint? Width = null;
    [UsedImplicitly] public uint? Height = null;
    [UsedImplicitly] public string? Upscaler = null;

    [UsedImplicitly] public int? GenerationTimeOutSeconds = null;
    [UsedImplicitly] public int? FastTimeOutSeconds = null;

    [UsedImplicitly] public uint? Image2ImageClipSkip = null;
    [UsedImplicitly] public uint? Text2ImageClipSkip = null;
    [UsedImplicitly] public int? RecheckDeadBackendTime = null;

    [UsedImplicitly] public ADetailer? AfterDetail = null;

    [UsedImplicitly]
    public class ADetailer
    {
        [UsedImplicitly] public float? HandMinSize;
        [UsedImplicitly] public float? FaceMinSize;
    }

    public class Backend
    {
        [UsedImplicitly] public bool Enabled;
        [UsedImplicitly] public string? Url;

        [UsedImplicitly] public int? GenerationTimeOutSeconds = null;
        [UsedImplicitly] public int? FastTimeOutSeconds = null;
        [UsedImplicitly] public float? StepsMultiplier = null;
    }
}

public class GlobalImageGenerationConfig
{
    [UsedImplicitly] public int? BatchSize;
}