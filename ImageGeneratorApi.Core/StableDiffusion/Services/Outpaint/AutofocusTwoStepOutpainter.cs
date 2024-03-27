using Autofocus;
using Autofocus.Config;
using Autofocus.ImageSharp.Extensions;
using Autofocus.Models;
using Autofocus.Outpaint;
using ImageGeneratorApi.Core.StableDiffusion.Interfaces;

namespace ImageGeneratorApi.Core.StableDiffusion.Services.Outpaint;

public class AutofocusTwoStepOutpainter : IImageOutpainter
{
    private readonly TwoStepOutpainter _outpainter;

    public AutofocusTwoStepOutpainter(IStableDiffusion api, IStableDiffusionModel model, ISampler sampler,
        int batchSize1, int batchSize2, int steps)
    {
        _outpainter = new TwoStepOutpainter(api, model, sampler)
        {
            BatchSize1 = batchSize1,
            BatchSize2 = batchSize2,
            Steps = steps,
            UseControlNetTile = false
        };
    }

    public async Task<IReadOnlyCollection<SixLabors.ImageSharp.Image>> Outpaint(SixLabors.ImageSharp.Image image,
        string positive, string negative, Func<ProgressReport, Task>? progressReporter = null)
    {
        progressReporter ??= _ => Task.CompletedTask;

        var base64 = await _outpainter.Outpaint(
            new PromptConfig
            {
                Positive = positive,
                Negative = negative
            },
            image,
            progressReporter
        );

        var results = new List<SixLabors.ImageSharp.Image>();
        foreach (var item in base64)
            results.Add(await item.ToImageSharpAsync());
        return results;
    }
}