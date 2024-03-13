using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ImageGeneratorApi.Domain.Common;

namespace ImageGeneratorApi.Domain.Entities;

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
    public ICollection<Image> Images { get; } = new List<Image>();
}