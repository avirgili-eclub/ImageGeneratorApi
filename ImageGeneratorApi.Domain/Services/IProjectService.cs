using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Services;

public interface IProjectService : IBaseService<Project>
{
    List<Project> GetAllByUser(string id);
    Task CreateProjectAsync(ProjectDto projectRequest);
}