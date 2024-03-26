using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Images.Repository;

public interface IImageRepository : IBaseRepository<Image>
{
    IQueryable<Image> GetAllByProjectId(int id);
}