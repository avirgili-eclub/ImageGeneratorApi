using Autofocus.Config;

namespace ImageGeneratorApi.Core.StableDiffusion.Interfaces;

public interface IImageAnalyser
{
    public Task<string> GetImageDescription(Stream image, InterrogateModel model);
}