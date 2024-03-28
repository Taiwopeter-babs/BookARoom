namespace BookARoom.Exceptions;

public sealed class AmenityNotFoundException : NotFoundException
{
    public AmenityNotFoundException(int amenityId) :
            base($"The amenity with the id: {amenityId} was not found")
    {
    }
}
