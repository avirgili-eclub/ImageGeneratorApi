using AutoMapper;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Domain.Services;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Image.Services;

public class ImageService : BaseService<Domain.Entities.Image>, IImageService
{
    
    private readonly IImageRepository _imageRepository;
    private readonly ILogger<ImageService> _logger;
    private readonly IMapper _mapper;
    
    public ImageService(ApplicationDbContext applicationDbContext, IImageRepository imageRepository, 
        ILogger<ImageService> logger, IMapper mapper) : base(applicationDbContext)
    {
        _imageRepository = imageRepository;
        _logger = logger;
        _mapper = mapper;
    }


    public List<Domain.Entities.Image> GetAllByProjectId(int id)
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

    public Domain.Entities.Image DtoToEntity(ImageDto imageRequest)
    {
        try
        {
            return _mapper.Map<Domain.Entities.Image>(imageRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while mapping image dto to entity");
            throw;
        }
    }
}