using ImageGeneratorApi.Core.StableDiffusion.Record;
using Image = SixLabors.ImageSharp.Image;
using System.Threading.Tasks;
using Autofocus.Outpaint;

namespace ImageGeneratorApi.Core.StableDiffusion.Interfaces;

public interface IImageGenerator
{
    Task<IReadOnlyCollection<SixLabors.ImageSharp.Image>> Text2Image(int? seed, Prompt prompt, Func<ProgressReport, Task>? progress = null, int batch = 1);

    Task<IReadOnlyCollection<SixLabors.ImageSharp.Image>> Image2Image(int? seed, SixLabors.ImageSharp.Image image, Prompt prompt, Func<ProgressReport, Task>? progress = null, int batch = 1);

    //public record struct ProgressReport(float Progress, MemoryStream? Intermediate);
}