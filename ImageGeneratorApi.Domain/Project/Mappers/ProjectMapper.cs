using AutoMapper;
using ImageGeneratorApi.Domain.Project.Dto;

namespace ImageGeneratorApi.Domain.Project.Mappers;

public class ProjectMapper : Profile
{
    public ProjectMapper()
    {
        CreateMap<Entities.Project, ProjectDto>().ReverseMap();
    }
}