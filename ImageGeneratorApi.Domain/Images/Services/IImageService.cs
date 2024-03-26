using ImageGeneratorApi.Domain.Common.Interfaces;
using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Images.Services;

public interface IImageService : IBaseService<Image>
{
    List<Image> GetAllByProjectId(int id);
    Image DtoToEntity(ImageDto imageRequest);
    Task CreateImages(ImageDto imageRequest, int projectId);
}