using System.ComponentModel.DataAnnotations.Schema;

namespace BookARoom.Models;

public class RoomsAmenities
{
    [Column("roomId")]
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;

    [Column("amenityId")]
    public int AmenityId { get; set; }
    public Amenity Amenity { get; set; } = null!;

    [Column("createdOn")]
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
}
