using AutoMapper;
using ImageGeneratorApi.Domain.Project.Dto;
using ImageGeneratorApi.Domain.Project.Repository;
using ImageGeneratorApi.Domain.Project.Services;
using ImageGeneratorApi.Infrastructure.Clients;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Project.Services;

public class ProjectService : BaseService<Domain.Project.Entities.Project>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ProjectService> _logger;
    private readonly IMapper _mapper;
 

    public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger, 
        IMapper mapper) : base(projectRepository)
    {
        _projectRepository = projectRepository;
        _logger = logger;
        _mapper = mapper;
 
    }

    public List<Domain.Project.Entities.Project> GetAllByUser(string id)
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

    public async Task CreateProjectAsync(string userId, ProjectDto projectRequest)
    {
        Domain.Project.Entities.Project project = _mapper.Map<Domain.Project.Entities.Project>(projectRequest);
        project.UserId = userId;
        var result = await CreateAsync(project);
        if (result == null)
        {
            //TODO: crear ProjectException
            throw new Exception("Error occurred while creating project");
        }
    }
}