using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Images.Enums;

namespace ImageGeneratorApi.Domain.Images.Entities;

public class Imagen : BaseEntity
{
    [Key]
    public int ImagenId { get; private set; }
    [MaxLength(100)]
    public required string Name { get; set; }
    [MaxLength(160)]
    public string? Description { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    [MaxLength(120)]
    public string? Path { get; set; }
    public int ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public required Project.Entities.Project Project { get; set; }
    [EnumDataType(typeof(ImageType))]
    public ImageType ImageType { get; set; }
}