using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Images.Enums;

namespace ImageGeneratorApi.Domain.Images.Dto;

public class ImageDto
{
    [MaxLength(100)]
    [Required]
    public required string Name { get; set; }
    [MaxLength(160)]
    [Required]
    public string? Description { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    // [MaxLength(120)]
    // public string? Path { get; set; }
    // public int ProjectId { get; set; }
    [EnumDataType(typeof(ImageType))]
    public ImageType ImageType { get; set; }
    [Required]
    public int Quantity { get; set; }
}