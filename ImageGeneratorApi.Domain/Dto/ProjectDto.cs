using System.ComponentModel.DataAnnotations;

namespace ImageGeneratorApi.Domain.Dto;

public class ProjectDto
{
    public required string Name { get; set; }
    [MaxLength(120)]
    public required string Description { get; set; }
    public string? UserId { get; set; }
    public ICollection<ImageDto> Images { get; } = new List<ImageDto>();
}