namespace BookARoom.Utilities;

public class GuestParameters : RequestParameters
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int MinBookings { get; set; } = 0;
    public int MaxBookings { get; set; } = int.MaxValue;
    public DateTime LastBookingDate { get; set; }

    /// <summary>
    /// The minimum date of guest creation from which to filter
    /// </summary>
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    [DateTimeLessThan("MaxCreationDate",
        ErrorMessage = "Date value cannot be greater than MaxCreationDate")]
    public DateTime MinCreationDate { get; set; }

    /// <summary>
    /// The maximum date of guest creation the filter ends
    /// </summary>
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    public DateTime MaxCreationDate { get; set; }
}
