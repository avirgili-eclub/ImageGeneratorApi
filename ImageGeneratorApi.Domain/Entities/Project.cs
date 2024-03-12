using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Common;

namespace ImageGeneratorApi.Domain.Entities;

public class Project : BaseEntity
{
    [Key]
    public int ProjectId { get; set; }
    public required string Name { get; set; }
    public required string Description { get; set; }
    public int UserId { get; set; }
    public required User User { get; set; }
    public ICollection<Image> Images { get; } = new List<Image>();
}