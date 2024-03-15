using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Project.Repository;

public class ProjectRepository : BaseRepository<Domain.Entities.Project>, IProjectRepository
{
    private readonly IApplicationDbContext _context;

    public ProjectRepository(IApplicationDbContext context)
    {
        _context = context;
    }

    public IQueryable<Domain.Entities.Project> GetAllByUser(string id)
    {
        return _context.Projects.Where(p => p.UserId == id);
    }
}