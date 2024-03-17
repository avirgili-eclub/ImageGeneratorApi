using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Services;

public interface IImageService : IBaseService<Image>
{
    List<Image> GetAllByProjectId(int id);
    Image DtoToEntity(ImageDto imageRequest);
}