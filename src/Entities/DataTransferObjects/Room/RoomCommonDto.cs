using System.ComponentModel.DataAnnotations;

namespace BookARoom.Dto;

public record RoomCommonDto
{
    [Required(ErrorMessage = "Room name is required")]
    [MaxLength(30, ErrorMessage = "Maximum length for name is 30 characters")]
    public string? Name { get; init; }

    [Required(ErrorMessage = "Room description is required")]
    [MaxLength(100, ErrorMessage = "Maximum length for description is 100 characters")]
    public string? Description { get; init; }

    [Required(ErrorMessage = "MaximumOccupancy value is required")]
    [Range(1, 4, ErrorMessage = "Maximum occupancy value must be between {1} and {2}")]
    public int MaximumOccupancy { get; init; }

    [Required(ErrorMessage = "Price value is required")]
    [Range(1.00, int.MaxValue, ErrorMessage = "Price value is required and must be between {1} and {2}")]
    public decimal Price { get; init; }

    [Required(ErrorMessage = "NumberAvailable value is required")]
    [Range(1, 10,
    ErrorMessage = "NumberAvailable value is required and must be between {1} and {2}")]
    public int NumberAvailable { get; init; }

    /// <summary>
    /// A list of ids of amenities to add
    /// </summary>
    public List<int>? Amenities { get; set; }
}
