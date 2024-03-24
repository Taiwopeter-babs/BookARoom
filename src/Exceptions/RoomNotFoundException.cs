namespace BookARoom.Exceptions;

public sealed class RoomNotFoundException : NotFoundException
{
    public RoomNotFoundException(int roomId) :
        base($"The room with the id: {roomId} was not found")
    {
    }
}
