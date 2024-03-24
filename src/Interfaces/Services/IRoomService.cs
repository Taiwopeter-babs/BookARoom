using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IRoomService
{
    Task<RoomDto> AddRoomAsync(RoomForCreationDto roomDto);

    Task<RoomDto> GetRoomAsync(int roomId, bool includeAmenity = false,
        bool trackChanges = false);

    Task<(IEnumerable<RoomDto>, PageMetadata pageMetadata)> GetRoomsAsync(
        RoomParameters roomParameters, bool trackChanges);
}