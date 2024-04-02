using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IRoomRepository
{
    void AddRoom(Room room);
    Task<Room?> GetRoomAsync(int roomId, bool includeAmenity, bool trackChanges = false);
    Task<PagedList<Room>> GetRoomsAsync(RoomParameters roomParameters, bool trackChanges = false);
    void RemoveRoom(Room room);
    void UpdateModifiedTime(Room room);

    Task<List<Room>> FindAvailableRooms(List<int> roomsId, bool trackChanges = false);
}