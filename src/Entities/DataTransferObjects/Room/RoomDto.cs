namespace BookARoom.Dto;

public record class RoomDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
    public string? Description { get; init; }
    public int MaximumOccupancy { get; init; }
    public decimal Price { get; init; }
    public int NumberAvailable { get; init; }
    public bool IsAvailable { get; init; }

    public List<AmenityDto>? Amenities { get; init; }
    // public List<BookingDto>? Bookings { get; init; }
}

