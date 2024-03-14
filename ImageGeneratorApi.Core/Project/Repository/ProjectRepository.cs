using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Infrastructure.Data.Repository;

namespace ImageGeneratorApi.Core.Project.Repository;

public class ProjectRepository : BaseRepository<Domain.Entities.Project>, IProjectRepository
{
    
}