using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Interfaces;

public interface IProjectRepository : IBaseRepository<Project>
{
    IQueryable<Project> GetAllByUser(string id);
}