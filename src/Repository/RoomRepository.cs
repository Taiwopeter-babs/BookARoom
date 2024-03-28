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
    /// <param name="includeAmenity">Set to false by default to exclude relationship entities</param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Room?> GetRoomAsync(int roomId, bool includeAmenity, bool trackChanges = false)
    {
        // Console.WriteLine($"{includeAmenity} {trackChanges}");
        return await FindByCondition(room => room.Id == roomId, trackChanges)
            .IncludeRelation(includeAmenity)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<Room>> GetRoomsAsync(RoomParameters roomParams,
        bool trackChanges)
    {
        var rooms = await FindAll(trackChanges)
            .FilterRoomsByPrice(roomParams.MinPrice, roomParams.MaxPrice)
            .FilterRoomsByNumberAvailable(roomParams.MinNumberAvailable, roomParams.MaxNumberAvailable)
            .OrderBy(room => room.Name)
            .ToListAsync();

        var roomsCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Room>.ToPagedList(rooms, roomsCount,
            roomParams.PageNumber, roomParams.PageSize);
    }

    public void RemoveRoom(Room room) => Delete(room);

    public void RemoveAmenities(List<Amenity> amenities) =>
        _bookARoomContext.RemoveRange(amenities);

    public async Task AddAmenities(List<Amenity> amenities) =>
        await _bookARoomContext.AddRangeAsync(amenities);

    /// <summary>
    /// Update the updatedAt field of the modified room
    /// </summary>
    /// <param name="entity"></param>
    public void UpdateModifiedTime(Room room) => UpdateTime(room);

}