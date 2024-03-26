using ImageGeneratorApi.Domain.Project.Repository;
using ImageGeneratorApi.Infrastructure.Data.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Project.Repository;

public class ProjectRepository : BaseRepository<Domain.Project.Entities.Project>, IProjectRepository
{
    private readonly IApplicationDbContext _context;

    public ProjectRepository(IApplicationDbContext context) : base(context)
    {
        _context = context;
    }

    public IQueryable<Domain.Project.Entities.Project> GetAllByUser(string id)
    {
        return _context.Projects.Where(p => p.UserId == id);
    }
}