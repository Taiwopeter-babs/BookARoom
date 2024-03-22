using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

[Table("bookings")]
public class Booking
{
    [Column("id")]
    public int Id { get; set; }

    [Column("checkinDate")]
    [Required]
    public DateTime? CheckinDate { get; set; }

    [Column("checkoutDate")]
    [Required]
    public DateTime? CheckoutDate { get; set; }

    // A reference to a guest's booking
    public Guest Guest { get; set; } = null!;

    public List<Room>? Rooms { get; set; }
}