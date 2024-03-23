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

    public async Task<Room?> GetRoomAsync(int roomId, bool includeRelation, bool trackChanges)
    {
        if (includeRelation)
            return await FindByCondition(room => room.Id == roomId, trackChanges)
            .IncludeAmenityAndBookingsRelation()
            .SingleOrDefaultAsync();

        return await FindByCondition(room => room.Id == roomId, trackChanges)
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