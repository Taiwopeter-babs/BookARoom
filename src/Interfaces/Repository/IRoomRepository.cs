using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IRoomRepository
{
    void AddRoom(Room room);
    Task<Room?> GetRoomAsync(int roomId, bool includeRelation, bool trackChanges);
    Task<PagedList<Room>> GetRoomsAsync(RoomParameters roomParameters, bool trackChanges);
    Task UpdateRoom(Room room);
    void RemoveRoom(Room room);
}