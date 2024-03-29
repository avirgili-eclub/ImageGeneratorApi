using ImageGeneratorApi.Domain.Images.Repository;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Image.Interfaces;

public class ImageRepository : BaseRepository<Domain.Images.Entities.Imagen>, IImageRepository
{
    public ImageRepository(IApplicationDbContext context) : base(context)
    {
    }
    
    public IQueryable<Domain.Images.Entities.Imagen> GetAllByProjectId(int id)
    {
        return Context.Images.Where(image => image.ProjectId == id);
    }
}