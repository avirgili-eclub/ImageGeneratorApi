using System.ComponentModel.DataAnnotations;
using ImageGeneratorApi.Domain.Common;

namespace ImageGeneratorApi.Domain.Entities;

public class User : BaseEntity
{
    [Key]
    public int UserId { get; set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Role { get; set; }
    public string? Token { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime TokenExpires { get; set; }
    public ICollection<Project> Projects { get; } = new List<Project>();
}