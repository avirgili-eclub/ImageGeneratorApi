using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Enums;

namespace ImageGeneratorApi.Domain.Dto;

public class ImageDto
{
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(160)]
    public string? Description { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    // [MaxLength(120)]
    // public string? Path { get; set; }
    // public int ProjectId { get; set; }
    [EnumDataType(typeof(ImageType))]
    public ImageType ImageType { get; set; }
}