using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Enums;

namespace ImageGeneratorApi.Domain.Entities;

public class Image : BaseEntity
{
    [Key]
    public int ImageId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public string? Path { get; set; }
    public int ProjectId { get; set; }
    public required Project Project { get; set; }
    public ImageType ImageType { get; set; }
}