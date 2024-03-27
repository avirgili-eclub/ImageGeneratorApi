using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageGeneratorApi.Domain.Common;
using ImageGeneratorApi.Domain.Common.Entities;
using ImageGeneratorApi.Domain.Images.Entities;

namespace ImageGeneratorApi.Domain.Project.Entities;

public class Project : BaseEntity
{
    [Key]
    public int ProjectId { get; private set; }
    [MaxLength(80)]
    public required string Name { get; set; }
    [MaxLength(120)]
    public required string Description { get; set; }
    public required string UserId { get; set; }
    [ForeignKey("Id")]
    public User User { get; set; }
    public ICollection<Imagen> Images { get; } = new List<Imagen>();
}