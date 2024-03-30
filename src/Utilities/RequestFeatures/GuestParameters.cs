namespace BookARoom.Utilities;

public class GuestParameters : RequestParameters
{
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
    public int MinBookings { get; init; } = 0;
    public int MaxBookings { get; init; } = int.MaxValue;
    public string LastBookingDate { get; init; } = "01/01/1000";

    /// <summary>
    /// The minimum date of guest creation from which to filter
    /// </summary>
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    [DateTimeLessThan("MaxCreationDate",
        ErrorMessage = "Date value cannot be greater than MaxCreationDate")]
    public string MinCreationDate { get; init; } = "01/01/1000";

    /// <summary>
    /// The maximum date of guest creation the filter ends
    /// </summary>
    [DateTimeValidation(ErrorMessage = "Invalid datetime value")]
    public string MaxCreationDate { get; init; } = "01/01/9999";
}
