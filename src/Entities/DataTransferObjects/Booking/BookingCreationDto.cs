﻿using System.ComponentModel.DataAnnotations;
using BookARoom.Utilities;

namespace BookARoom.Dto;

public record class BookingCreationDto
{
    [Required]
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    [DateTimeLessThan("CheckoutDate", ErrorMessage = "CheckinDate cannot be greater than Checkout")]
    public DateTime CheckinDate { get; init; }

    [Required]
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    public DateTime CheckoutDate { get; init; }

    /// <summary>
    /// Id of the guest making the booking
    /// </summary>
    public int GuestId { get; init; }

    /// <summary>
    /// The room(s) booked by a client. Contains information on the room id,
    /// number of guests, and the number available for the room type
    /// </summary>
    public List<RoomBookingDto>? Rooms { get; init; }
}