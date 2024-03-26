using System.ComponentModel.DataAnnotations;

namespace BookARoom;

public record RoomForUpdateDto
{
    [Required(ErrorMessage = "Room name is required")]
    [MaxLength(30, ErrorMessage = "Maximum length for name is 30 characters")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Room description is required")]
    [MaxLength(100, ErrorMessage = "Maximum length for description is 100 characters")]
    public string? Description { get; init; }

    [Required(ErrorMessage = "MaximumOccupancy value is required")]
    public int MaximumOccupancy { get; init; }

    [Required(ErrorMessage = "Price value is required")]
    public decimal Price { get; init; }

    [Required(ErrorMessage = "NumberAvailable value is required")]
    public int NumberAvailable { get; init; }
}
