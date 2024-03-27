namespace ImageGeneratorApi.Core.StableDiffusion.Exception;

public abstract class ImageGenerationException(string message)
    : System.Exception(message);

public class ImageGenerationPrivateChannelRequiredException()
    : ImageGenerationException("I'm sorry I can't generate that image.");