namespace BookARoom.Exceptions;

public abstract class BadRequestException : Exception
{
    public BadRequestException(string message) : base(message) { }
}

public sealed class AmenityAlreadyExistsException : BadRequestException
{
    public AmenityAlreadyExistsException(string amenityName) :
        base($"The amenity with the name: {amenityName}, already exists")
    { }
}
