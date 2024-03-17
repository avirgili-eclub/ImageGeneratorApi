using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Image.Interfaces;

public class ImageRepository : BaseRepository<Domain.Entities.Image>, IImageRepository
{
    private readonly IApplicationDbContext _context;

    public ImageRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Domain.Entities.Image> GetAllByProjectId(int id)
    {
        return _context.Images.Where(i => i.ProjectId == id);
    }
}