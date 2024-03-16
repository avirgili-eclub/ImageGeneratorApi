using AutoMapper;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Mappers;

public class ImageMapper : Profile
{
    public ImageMapper()
    {
        CreateMap<Image, ImageDto>().ReverseMap();
    }
}