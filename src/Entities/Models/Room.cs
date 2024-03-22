using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

[Table("rooms")]
public class Room : BaseModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    [MaxLength(128)]
    public string? Name { get; set; }

    [Column("description")]
    [Required]
    [MaxLength(128)]
    public string? Description { get; set; }

    [Column("maxOccupancy")]
    [Required]
    public int MaxOccupancy { get; set; }

    [Column("numberAvailable")]
    [Required]
    public int NumberAvailable { get; set; }

    [Column("price")]
    [Required]
    public decimal Price { get; set; }

    [Column("state")]
    [Required]
    public string? State { get; set; }

    // many-many rooms and amenities
    public List<Amenity>? Amenities { get; set; }

    // many-many rooms and bookings
    public List<Booking>? Bookings { get; set; }
}