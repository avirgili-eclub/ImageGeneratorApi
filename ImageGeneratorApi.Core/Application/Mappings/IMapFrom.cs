using AutoMapper;

namespace ImageGeneratorApi.Core.Application.Mappings;

public interface IMapFrom<T>
{
    void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());

}