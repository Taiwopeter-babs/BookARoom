using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IRoomRepository
{
    void AddRoom(Room room);
    Task<Room?> GetRoomAsync(int roomId, bool includeAmenity = false, bool trackChanges = false);
    Task<PagedList<Room>> GetRoomsAsync(RoomParameters roomParameters, bool trackChanges = false);
    void RemoveRoom(Room room);
}