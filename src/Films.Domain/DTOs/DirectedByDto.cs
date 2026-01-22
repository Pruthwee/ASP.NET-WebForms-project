namespace Films.Domain.DTOs;

public class DirectedByDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? ModifiedDate { get; set; }
    public bool IsActive { get; set; }
}

public class DirectedByCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
}

public class DirectedByUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public bool IsActive { get; set; }
}
