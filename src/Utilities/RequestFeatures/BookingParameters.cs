namespace BookARoom.Utilities;

public class BookingParameters : RequestParameters
{
    public string BookingDate { get; init; } = "01/01/2024";
    public string CheckinDate { get; init; } = "01/01/2024";
    public string CheckoutDate { get; init; } = "01/01/2024";
}
