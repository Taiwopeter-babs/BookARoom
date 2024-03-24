using BookARoom.Data;
using BookARoom.Extensions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Repository;

public class RoomRepository : RepositoryBase<Room>, IRoomRepository
{
    public RoomRepository(BookARoomContext context) : base(context)
    {
    }

    public void AddRoom(Room room) => Create(room);

    /// <summary>
    /// Gets a Room.
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="includeRelation">Set to false by default to exclude relationship entities</param>
    /// <param name="includeBooking">Includes the Booking entities if set to true</param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Room?> GetRoomAsync(int roomId, bool includeAmenity = false, bool trackChanges = false)
    {
        return await FindByCondition(room => room.Id == roomId, trackChanges)
            .IncludeBookingsRelation(includeAmenity)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<Room>> GetRoomsAsync(RoomParameters roomParameters,
        bool trackChanges)
    {
        var rooms = await FindAll(trackChanges)
            .OrderBy(room => room.Name)
            .ToListAsync();

        var roomsCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Room>.ToPagedList(rooms, roomsCount,
            roomParameters.PageNumber, roomParameters.PageSize);
    }

    public void RemoveRoom(Room room) => Delete(room);

    public async Task UpdateRoom(Room room)
    {
        throw new NotImplementedException();
    }
}