using Autofocus.Outpaint;

namespace ImageGeneratorApi.Core.StableDiffusion.Interfaces;

public interface IImageUpscaler
{
    public Task<SixLabors.ImageSharp.Image> UpscaleImage(SixLabors.ImageSharp.Image image, uint width, uint height,
        Func<ProgressReport, Task>? progressReporter = null);
}