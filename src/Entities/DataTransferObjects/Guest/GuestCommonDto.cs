using System.ComponentModel.DataAnnotations;

namespace BookARoom.Dto;

public record GuestCommonDto
{
    [Required(ErrorMessage = "First name is required")]
    [MaxLength(128, ErrorMessage = "Maximum length for first name is 128 characters")]
    public string? FirstName { get; set; }

    [Required(ErrorMessage = "Last name is required")]
    [MaxLength(128, ErrorMessage = "Maximum length for last name is 128 characters")]
    public string? LastName { get; set; }

    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "Country is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for country is 60 characters")]
    public string? Country { get; set; }

    [Required(ErrorMessage = "City is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for city is 60 characters")]
    public string? City { get; set; }

    [Required(ErrorMessage = "State is required")]
    [MaxLength(60, ErrorMessage = "Maximum length for state is 60 characters")]
    public string? State { get; set; }
}

public record GuestCreationDto : GuestCommonDto { }

public record GuestUpdateDto : GuestCommonDto
{
    // public DateTime? LastBookingDate { get; set; }

    // public int NumberOfBookings { get; set; }
}
