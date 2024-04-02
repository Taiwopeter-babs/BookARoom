using System.ComponentModel.DataAnnotations;
using BookARoom.Utilities;

namespace BookARoom.Dto;

public abstract record BookingCommonDto
{
    [Required]
    [DateTimeValidation(ErrorMessage = "Invalid datetime value. Accepted Format: dd/MM/yyyy")]
    [DateTimeLessThan("CheckoutDate", ErrorMessage = "CheckinDate cannot be greater than CheckoutDate")]
    public string? CheckinDate { get; init; }

    [Required]
    [DateTimeValidation(ErrorMessage = "Invalid datetime value. Accepted Format: dd/MM/yyyy")]
    public string? CheckoutDate { get; init; }

    /// <summary>
    /// Id of the guest making the booking
    /// </summary>
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Invalid guest id integer")]
    public int GuestId { get; init; }

    /// <summary>
    /// The room(s) booked by a client. Contains information on the room id,
    /// number of guests, and the number available for the room type
    /// </summary>
    [Required]
    public List<RoomBookingDto>? Rooms { get; init; }
}

public record BookingCreationDto : BookingCommonDto { }


public record BookingUpdateDto : BookingCommonDto { }
