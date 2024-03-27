using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Images.Services;

public interface IProjectImageService : IBaseService<Imagen>
{
    List<Imagen> GetAllByProjectId(int id);
    Imagen DtoToEntity(ImagenDto imagenRequest);
    Task CreateImages(ImagenDto imagenRequest, int projectId);
}