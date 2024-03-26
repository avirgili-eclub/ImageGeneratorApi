using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Project.Dto;

namespace ImageGeneratorApi.Domain.Project.Services;

public interface IProjectService : IBaseService<Project.Entities.Project>
{
    List<Project.Entities.Project> GetAllByUser(string id);
    Task CreateProjectAsync(string userId, ProjectDto projectRequest);
    
}