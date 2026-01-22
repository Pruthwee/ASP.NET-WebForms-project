using Microsoft.AspNetCore.Identity;

namespace Films.Domain.Entities;

/// <summary>
/// Represents an application user with Identity support
/// </summary>
public class User : IdentityUser<int>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? TypeUserId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; } = true;
    public string CreatedBy { get; set; } = "System";
    public string? ModifiedBy { get; set; }

    // Navigation properties
    public virtual TypeUser? TypeUser { get; set; }
    public virtual ICollection<UserRight> UserRights { get; set; } = new List<UserRight>();
}
