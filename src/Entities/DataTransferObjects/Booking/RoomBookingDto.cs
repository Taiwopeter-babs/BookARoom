using System.ComponentModel.DataAnnotations;

namespace BookARoom.Dto;

/// <summary>
/// Contains the information from the http client for each room booked
/// </summary>
public record RoomBookingDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid id value")]
    public int RoomId { get; init; }

    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid number of guests")]
    public int NumberGuests { get; init; }


    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid number of rooms")]
    public int NumberRooms { get; init; }
}
