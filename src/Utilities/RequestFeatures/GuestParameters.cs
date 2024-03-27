namespace BookARoom.Utilities;

public class GuestParameters : RequestParameters
{
    public string? FirstName { get; set; }
    public string? Country { get; set; }
    public string? City { get; set; }
    public string? State { get; set; }
}
