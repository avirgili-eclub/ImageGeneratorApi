using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Images.Repository;

public interface IImageRepository : IBaseRepository<Imagen>
{
    IQueryable<Imagen> GetAllByProjectId(int id);
}