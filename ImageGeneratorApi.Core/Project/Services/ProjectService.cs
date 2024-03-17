using AutoMapper;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Interfaces;
using ImageGeneratorApi.Domain.Services;
using ImageGeneratorApi.Infrastructure.Data.Services;
using ImageGeneratorApi.Infrastructure.Persistence;
using Microsoft.Extensions.Logging;

namespace ImageGeneratorApi.Core.Project.Services;

public class ProjectService : BaseService<Domain.Entities.Project>, IProjectService
{
    private readonly IProjectRepository _projectRepository;
    private readonly ILogger<ProjectService> _logger;
    private readonly IMapper _mapper;

    public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger, IMapper mapper, 
        ApplicationDbContext applicationDbContext) : base(applicationDbContext)
    {
        _projectRepository = projectRepository;
        _logger = logger;
        _mapper = mapper;
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

    public async Task CreateProjectAsync(string userId, ProjectDto projectRequest)
    {
        Domain.Entities.Project project = _mapper.Map<Domain.Entities.Project>(projectRequest);
        project.UserId = userId;
        var result = await CreateAsync(project);
        if (result == null)
        {
            //TODO: crear ProjectException
            throw new Exception("Error occurred while creating project");
        }
    }
}