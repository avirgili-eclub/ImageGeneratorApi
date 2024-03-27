namespace ImageGeneratorApi.Core.StableDiffusion.Enums;

public enum ImageGenerationType
{
    /// <summary>
    /// Prompt (plus reference, if not null) will be used to generate an image.
    /// </summary>
    Generate,

    /// <summary>
    /// Reference image will be upscaled.
    /// </summary>
    Upscale,

    /// <summary>
    /// Image will be outpainted (expanded out in all directions)
    /// </summary>
    Outpaint
}