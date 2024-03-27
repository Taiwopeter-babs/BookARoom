namespace BookARoom.Interfaces;

public interface IServiceManager
{
    IRoomService RoomService { get; }
    IAmenityService AmenityService { get; }
}
