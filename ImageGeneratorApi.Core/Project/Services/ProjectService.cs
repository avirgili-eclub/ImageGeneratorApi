using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Domain.Services;
using ImageGeneratorApi.Infrastructure.Data.Services;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Project.Services;

public class ProjectService : BaseService<Domain.Entities.Project>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger)
    {
        _projectRepository = projectRepository;
        _logger = logger;
    }

    public List<Domain.Entities.Project> GetAllByUser(string id)
    {
        try
        {
            return _projectRepository.GetAllByUser(id).ToList();
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error occurred while getting projects by user");
            throw;
        }
    }

    public Task CreateProjectAsync(ProjectDto projectRequest)
    {
        //TODO: implementar mapper para mapear DTO a Entity
        throw new NotImplementedException();
    }
}