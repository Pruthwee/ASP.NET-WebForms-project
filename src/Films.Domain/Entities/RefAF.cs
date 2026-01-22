namespace Films.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between Actors and Films
/// </summary>
public class RefAF
{
    public int Id { get; set; }
    public int ActorId { get; set; }
    public int FilmId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";

    // Navigation properties
    public virtual Actor Actor { get; set; } = null!;
    public virtual Film Film { get; set; } = null!;
}
