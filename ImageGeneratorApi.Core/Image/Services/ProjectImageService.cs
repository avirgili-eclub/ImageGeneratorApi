using AutoMapper;
using Discord.WebSocket;
using ImageGeneratorApi.Core.StableDiffusion.Config;
using ImageGeneratorApi.Core.StableDiffusion.Enums;
using ImageGeneratorApi.Core.StableDiffusion.Extensions;
using ImageGeneratorApi.Core.StableDiffusion.Interfaces;
using ImageGeneratorApi.Core.StableDiffusion.Services;
using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Entities;
using ImageGeneratorApi.Domain.Images.Repository;
using ImageGeneratorApi.Domain.Images.Services;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Image.Services;

public class ProjectImageService : BaseService<Imagen>, IProjectImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<ProjectImageService> _logger;
    private readonly IMapper _mapper;
    private readonly IImageAnalyser _analyser;
    private readonly HttpClient _http;
    private readonly StableDiffusionBackendCache _backends;
    private readonly IImageGenerationConfigStorage _storage;
    private readonly IImageGenerator _generator;
    private readonly IImageUpscaler _upscaler;
    private readonly IImageOutpainter _outpainter;

    public ProjectImageService(IImageRepository imageRepository,
        ILogger<ProjectImageService> logger, IMapper mapper, IImageAnalyser analyser, HttpClient http,
        StableDiffusionBackendCache backends, IImageGenerationConfigStorage storage, IImageGenerator generator,
        IImageUpscaler upscaler, IImageOutpainter outpainter) : base(imageRepository)
    {
        _imageRepository = imageRepository;
        _logger = logger;
        _mapper = mapper;
        _analyser = analyser;
        _http = http;
        _backends = backends;
        _storage = storage;
        _generator = generator;
        _upscaler = upscaler;
        _outpainter = outpainter;
    }


    public List<Imagen> GetAllByProjectId(int id)
    {
        try
        {
            return _imageRepository.GetAllByProjectId(id).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while getting images by project");
            throw;
        }
    }

    public Domain.Images.Entities.Imagen DtoToEntity(ImagenDto imagenRequest)
    {
        try
        {
            return _mapper.Map<Imagen>(imagenRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while mapping image dto to entity");
            throw;
        }
    }

    public async Task CreateImages(ImagenDto imagenRequest, int projectId)
    {
        try
        {
            List<Imagen> imagenes = new();
            for (int i = 0; i < imagenRequest.Quantity; i++)
            {
                string positive = imagenRequest.Name + ", " + imagenRequest.Description;
                // Get the config that was used to generate this.
                // If it's null it's probably because a legacy style button was pressed, try to make up a best-guess config.
                var config = await LegacyConfig(positive);

                // // Nothing needs doing for redo button, config fetched from storage is already correct
                // if (buttonType != MidjourneyStyleImageGenerationButtons.RedoButtonId)
                // {
                //     // Get the attachment the button wants to work on
                //     config.ReferenceImageUrl = (await GetAttachment(args, parsedButtonIndex))?.Url;
                //
                //     // Pick generation type
                //     if (buttonType.StartsWith(MidjourneyStyleImageGenerationButtons.VariantButtonId))
                //         config.Type = ImageGenerationType.Generate;
                //     else if (buttonType.StartsWith(MidjourneyStyleImageGenerationButtons.UpscaleButtonId))
                //         config.Type = ImageGenerationType.Upscale;
                //     else if (buttonType.StartsWith(MidjourneyStyleImageGenerationButtons.OutpaintButtonId))
                //         config.Type = ImageGenerationType.Outpaint;
                //     else
                //         throw new InvalidOperationException($"Unknown button type: {buttonType}");
                // }

                // Do the actual generation!
                await new SocketMessageComponentGenerationContext(config, _generator, _upscaler, _outpainter, _http,
                    _storage).Run();
                Imagen entity = DtoToEntity(imagenRequest);
                entity.ProjectId = projectId;
                imagenes.Add(entity);
            }

            await _imageRepository.CreateRangeAsync(imagenes);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while creating images");
            throw;
        }
    }

    #region helpers

    private async Task<ImageGenerationConfig> LegacyConfig(string positive)
    {
        // Lets just assume the message is the prompt
        var negative = "(nsfw), (spider), (nude)";

        // Try to extract the prompt from the image attachments
        // var firstImage = args.Message.Attachments.FirstOrDefault(a => a.ContentType.StartsWith("image/"));
        // if (firstImage != null)
        // {
        //     var image = await SixLabors.ImageSharp.Image.LoadAsync(await _http.GetStreamAsync(firstImage.Url));
        //     var prompt = image.GetGenerationPrompt();
        //     if (prompt != null)
        //         (positive, negative) = prompt.Value;
        // }

        return new ImageGenerationConfig
        {
            BatchSize = 2,
            IsPrivate = true,
            Positive = positive,
            Negative = negative,
            ReferenceImageUrl = null,
            Type = ImageGenerationType.Generate
        };
    }

    #endregion
}