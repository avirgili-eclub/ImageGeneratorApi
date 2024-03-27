namespace ImageGeneratorApi.Core.StableDiffusion.Interfaces;

public interface IImageGeneratorBannedWords
{
    bool IsBanned(string prompt);
}