namespace BookARoom.Exceptions;

public abstract class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
}

public sealed class AmenityNotFoundException : NotFoundException
{
    public AmenityNotFoundException(int amenityId) :
            base($"The amenity with the id: {amenityId} was not found")
    {
    }
}

public class GuestNotFoundException : NotFoundException
{
    public GuestNotFoundException(int guestId) :
            base($"The guest with the id: {guestId} was not found")
    {
    }
}

public sealed class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(int roomId) :
        base($"The room with the id: {roomId} was not found")
    {
    }
}