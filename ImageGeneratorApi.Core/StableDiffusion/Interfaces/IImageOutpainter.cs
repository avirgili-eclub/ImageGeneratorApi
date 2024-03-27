using Autofocus.Outpaint;

namespace ImageGeneratorApi.Core.StableDiffusion.Interfaces;

public interface IImageOutpainter
{
    public Task<IReadOnlyCollection<SixLabors.ImageSharp.Image>> Outpaint(SixLabors.ImageSharp.Image image,
        string positive, string negative, Func<ProgressReport, Task>? progressReporter = null);
}