namespace BookARoom.Dto;

/// <summary>
/// Contains the information from the http client for each room booked
/// </summary>
public record RoomBookingDto
{
    public int RoomId { get; set; }
    public int NumberGuests { get; set; }
    public int NumberRooms { get; set; }
}
