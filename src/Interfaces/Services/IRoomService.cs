using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IRoomService
{
    Task<RoomDto> AddRoomAsync(RoomCreationDto roomDto);

    Task<RoomDto> GetRoomAsync(int roomId, bool includeAmenity,
        bool trackChanges = false);

    Task<(IEnumerable<RoomDto>, PageMetadata pageMetadata)> GetRoomsAsync(
        RoomParameters roomParameters, bool trackChanges = false);

    Task UpdateRoomAsync(int roomId, RoomUpdateDto roomForUpdateDto,
        bool trackChanges = true);

    Task RemoveRoomAsync(int roomId, bool trackChanges = false);

    // Task BookRoom(RoomBookingDto roomToBook);
}