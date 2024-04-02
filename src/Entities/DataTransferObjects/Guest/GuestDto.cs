namespace BookARoom.Dto;

public record GuestDto
{
    public int Id { get; init; }

    /// <summary>
    /// FirstName + LastName
    /// </summary>
    public string? FullName { get; init; }

    public string? Email { get; init; }

    /// <summary>
    /// Country + City + State
    /// </summary>
    public string? Location { get; init; }

    public DateTime? LastBookingDate { get; init; }

    public int NumberOfBookings { get; init; }

    /// <summary>
    /// Gives information on whether a guest is a new or old customer 
    /// </summary>
    public bool NewGuest { get; set; } = false;
}
