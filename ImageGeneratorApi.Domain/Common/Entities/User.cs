using Microsoft.AspNetCore.Identity;

namespace ImageGeneratorApi.Domain.Common.Entities;

public class User : IdentityUser
{
    public DateTime CreatedAt { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public bool IsDeleted { get; set; }
    public string? DeletedBy { get; set; }
    public DateTime? DeletedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? CreatedBy { get; set; }
    public ICollection<Project.Entities.Project> Projects { get; } = new List<Project.Entities.Project>();
}