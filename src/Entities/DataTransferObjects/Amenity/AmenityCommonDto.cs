using System.ComponentModel.DataAnnotations;

namespace BookARoom.Dto;

public record AmenityCommonDto
{
    [Required(ErrorMessage = "Amenity name is required")]
    [MaxLength(100, ErrorMessage = "Maximum length for name is 100 characters")]
    public string? Name { get; set; }
}

public record AmenityCreationDto : AmenityCommonDto
{
}

public record AmenityUpdateDto : AmenityCommonDto
{

}
