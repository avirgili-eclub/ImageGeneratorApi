using AutoMapper;
using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Repository;
using ImageGeneratorApi.Domain.Images.Services;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Image.Services;

public class ImageService : BaseService<Domain.Images.Entities.Image>, IImageService
{
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<ImageService> _logger;
    private readonly IMapper _mapper;

    public ImageService(IImageRepository imageRepository,
        ILogger<ImageService> logger, IMapper mapper) : base(imageRepository)
    {
        _imageRepository = imageRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public List<Domain.Images.Entities.Image> GetAllByProjectId(int id)
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

    public Domain.Images.Entities.Image DtoToEntity(ImageDto imageRequest)
    {
        try
        {
            return _mapper.Map<Domain.Images.Entities.Image>(imageRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while mapping image dto to entity");
            throw;
        }
    }

    public async Task CreateImages(ImageDto imageRequest, int projectId)
    {
        try
        {
            List<Domain.Images.Entities.Image> images = new();
            for (int i = 0; i < imageRequest.Quantity; i++)
            {
                Domain.Images.Entities.Image entity = DtoToEntity(imageRequest);
                entity.ProjectId = projectId;
                images.Add(entity);
            }
            await _imageRepository.CreateRangeAsync(images);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while creating images");
            throw;
        }
    }
}