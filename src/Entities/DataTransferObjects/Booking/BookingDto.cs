
namespace BookARoom.Dto;

public record BookingDto
{
    public int Id { get; init; }
    public DateTime BookingDate { get; init; }
    public DateTime CheckinDate { get; init; }
    public DateTime CheckoutDate { get; init; }

    public GuestDto? Guest { get; init; }
    public List<RoomDto>? RoomsBooked { get; set; }

}
