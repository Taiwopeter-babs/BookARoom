using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

[Table("guests")]
public class Guest : BaseModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("firstName")]
    [Required]
    [MaxLength(128)]
    public required string FirstName { get; set; }

    [Column("lastName")]
    [Required]
    [MaxLength(128)]
    public required string LastName { get; set; }

    [Column("email")]
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Column("country")]
    [Required]
    [MaxLength(60)]
    public required string Country { get; set; }

    [Column("city")]
    [Required]
    [MaxLength(60)]
    public required string City { get; set; }

    [Column("state")]
    [Required]
    [MaxLength(60)]
    public required string State { get; set; }

    [Column("lastBookingDate")]
    public DateTime? LastBookingDate { get; set; }

    [Column("numberOfBookings")]
    public int NumberOfBookings { get; set; }

    // A one to many between a guest and bookings
    public ICollection<Booking> Bookings { get; set; } = [];
}