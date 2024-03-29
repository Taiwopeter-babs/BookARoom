namespace BookARoom.Dto;

public record AmenityDto
{
    public int Id { get; init; }
    public string? Name { get; init; }

    public DateTime? CreatedAt { get; init; }
    public DateTime? UpdatedAt { get; init; }
}
