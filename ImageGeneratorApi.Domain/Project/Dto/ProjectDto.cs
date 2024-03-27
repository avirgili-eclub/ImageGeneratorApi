using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Images.Dto;

namespace ImageGeneratorApi.Domain.Project.Dto;

public class ProjectDto
{
    public required string Name { get; set; }
    [MaxLength(120)]
    public required string Description { get; set; }
    public string? UserId { get; set; }
    public ICollection<ImagenDto> Images { get; } = new List<ImagenDto>();
}