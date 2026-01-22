namespace Films.Domain.Entities;

/// <summary>
/// Represents the many-to-many relationship between Directors, Actors, and Films
/// </summary>
public class RefDAF
{
    public int Id { get; set; }
    public int DirectedById { get; set; }
    public int FilmId { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public string CreatedBy { get; set; } = "System";

    // Navigation properties
    public virtual DirectedBy DirectedBy { get; set; } = null!;
    public virtual Film Film { get; set; } = null!;
}
