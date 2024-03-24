namespace BookARoom.Dto;

public record RoomForCreationDto
{
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int MaximumOccupancy { get; init; }
    public decimal Price { get; init; }
    public int NumberAvailable { get; init; }
}
