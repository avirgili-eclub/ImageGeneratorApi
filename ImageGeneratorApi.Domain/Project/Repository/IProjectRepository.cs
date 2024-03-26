using ImageGeneratorApi.Domain.Common.Interfaces;

namespace ImageGeneratorApi.Domain.Project.Repository;

public interface IProjectRepository : IBaseRepository<Entities.Project>
{
    IQueryable<Entities.Project> GetAllByUser(string id);
}