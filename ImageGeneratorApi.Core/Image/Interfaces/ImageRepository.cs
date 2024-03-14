using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Image.Interfaces;

public class ImageRepository : BaseRepository<Domain.Entities.Image>, IImageRepository
{
    
}