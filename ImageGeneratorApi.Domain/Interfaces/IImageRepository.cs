using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Interfaces;

public interface IImageRepository : IBaseRepository<Image>
{
    IQueryable<Image> GetAllByProjectId(int id);
}