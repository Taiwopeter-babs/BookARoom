using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

public class RoomsBookings
{
    [Column("roomId")]
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Column("bookingId")]
    public int BookingId { get; set; }
    public Booking Booking { get; set; } = null!;

    [Column("createdOn")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
