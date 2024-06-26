using System.ComponentModel;
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

    [Column("maximumOccupancy")]
    [Required]
    public int MaximumOccupancy { get; set; }

    [Column("numberAvailable")]
    [Required]
    public int NumberAvailable { get; set; }

    [Column("price")]
    [Required]
    public decimal Price { get; set; }

    [Column("isAvailable")]
    [Required]
    [DefaultValue(true)]
    public bool IsAvailable { get; set; } = true;

    // many-many rooms and amenities
    public List<Amenity> Amenities { get; set; } = [];
    public List<RoomsAmenities> RoomsAmenities { get; set; } = [];

    // many-many rooms and bookings
    public List<Booking> Bookings { get; set; } = [];
    public List<RoomsBookings> RoomsBookings { get; set; } = [];
}