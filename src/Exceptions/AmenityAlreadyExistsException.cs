namespace BookARoom.Exceptions;

public sealed class AmenityAlreadyExistsException : BadRequestException
{
    public AmenityAlreadyExistsException(string amenityName) :
        base($"The amenity with the name: {amenityName}, already exists")
    { }
}
