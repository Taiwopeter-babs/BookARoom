namespace BookARoom.Dto;

public record class GuestDto
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
}
