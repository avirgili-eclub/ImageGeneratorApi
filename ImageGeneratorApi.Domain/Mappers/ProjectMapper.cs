using AutoMapper;
using ImageGeneratorApi.Domain.Dto;
using ImageGeneratorApi.Domain.Entities;

namespace ImageGeneratorApi.Domain.Mappers;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Project, ProjectDto>().ReverseMap();
    }
}