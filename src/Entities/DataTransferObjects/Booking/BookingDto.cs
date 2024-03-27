﻿
namespace BookARoom.Dto;

public record BookingDto
{
    public int Id { get; init; }
    public DateTime BookingDate { get; init; }
    public DateTime CheckinDate { get; init; }
    public int CheckinTime { get; init; }
    public DateTime CheckoutDate { get; init; }
    public int CheckoutTime { get; init; }
    public GuestDto? Guest { get; init; }

    public List<RoomBookingDto>? RoomsBooked { get; init; }

}
