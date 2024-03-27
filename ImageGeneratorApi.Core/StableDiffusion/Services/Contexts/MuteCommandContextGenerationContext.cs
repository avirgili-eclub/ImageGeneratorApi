using ImageGeneratorApi.Core.StableDiffusion.Config;
using ImageGeneratorApi.Core.StableDiffusion.Interfaces;
using Discord;

namespace ImageGeneratorApi.Core.StableDiffusion.Services.Contexts;

public class MuteCommandContextGenerationContext
    : BaseImageGenerationContext
{
    private readonly IUserMessage _reply;
    
    public MuteCommandContextGenerationContext(ImageGenerationConfig config, IImageGenerator generator, IImageUpscaler upscaler, IImageOutpainter outpainter, HttpClient http, IUserMessage reply, IImageGenerationConfigStorage storage)
        : base(config, storage, generator, upscaler, outpainter, http)
    {
        _reply = reply;
    }

    protected override Task ModifyReply(Action<MessageProperties> modify)
    {
        return _reply.ModifyAsync(modify);
    }
}