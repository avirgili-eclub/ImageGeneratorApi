using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Enums;

namespace ImageGeneratorApi.Domain.Entities;

public class Image : BaseEntity
{
    [Key]
    public int ImageId { get; private set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(160)]
    public string? Description { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    [MaxLength(120)]
    public string? Path { get; set; }
    public int ProjectId { get; set; }
    public required Project Project { get; set; }
    [EnumDataType(typeof(ImageType))]
    public ImageType ImageType { get; set; }
}