using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

[Table("amenities")]
public class Amenity : BaseModel
{
    [Column("id")]
    public int Id { get; set; }

    [Column("name")]
    [Required]
    public string? Name { get; set; }

    public List<Room> Rooms { get; set; } = [];
    public List<RoomsAmenities> RoomsAmenities { get; set; } = [];
}