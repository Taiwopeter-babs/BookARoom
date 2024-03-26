using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

[Table("guests")]
public class Guest
{
    [Column("id")]
    public int Id { get; set; }

    [Column("firstName")]
    [Required]
    [MaxLength(128)]
    public string? FirstName { get; set; }

    [Column("lastName")]
    [Required]
    [MaxLength(128)]
    public string? LastName { get; set; }

    [Column("email")]
    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Column("country")]
    [Required]
    [MaxLength(60)]
    public string? Country { get; set; }

    [Column("city")]
    [Required]
    [MaxLength(60)]
    public string? City { get; set; }

    [Column("state")]
    [Required]
    [MaxLength(60)]
    public string? State { get; set; }

    // A one to one between a guest and a booking
    public Booking? Booking { get; set; }
}