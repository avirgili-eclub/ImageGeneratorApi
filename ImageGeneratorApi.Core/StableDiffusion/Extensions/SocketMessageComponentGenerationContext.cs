using Discord;
using Discord.WebSocket;
using ImageGeneratorApi.Core.StableDiffusion.Config;
using ImageGeneratorApi.Core.StableDiffusion.Interfaces;
using ImageGeneratorApi.Core.StableDiffusion.Services.Contexts;

namespace ImageGeneratorApi.Core.StableDiffusion.Extensions;

// TODO: Implement the SocketMessageComponentGenerationContext class
public class SocketMessageComponentGenerationContext
    : BaseImageGenerationContext
{
    public SocketMessageComponentGenerationContext(ImageGenerationConfig config, IImageGenerator generator,
        IImageUpscaler upscaler, IImageOutpainter outpainter, HttpClient http ,
        IImageGenerationConfigStorage storage)
        : base(config, storage, generator, upscaler, outpainter, http)
    {
    }
    
    protected override Task ModifyReply(Action<MessageProperties> modify)
    {
        throw new NotImplementedException();
    }
}