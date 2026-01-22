namespace Films.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between Users and Rights
/// </summary>
public class UserRight
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int RightId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";

    // Navigation properties
    public virtual User User { get; set; } = null!;
    public virtual Right Right { get; set; } = null!;
}
