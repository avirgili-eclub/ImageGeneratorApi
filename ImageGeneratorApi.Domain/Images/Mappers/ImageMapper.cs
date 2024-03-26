using AutoMapper;
using ImageGeneratorApi.Domain.Images.Dto;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Images.Mappers;

public class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<Image, ImageDto>().ReverseMap();
    }
}