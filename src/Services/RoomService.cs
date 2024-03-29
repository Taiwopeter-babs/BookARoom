using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Services;

internal sealed class RoomService : IRoomService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;

    public RoomService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<RoomDto> AddRoomAsync(RoomCreationDto roomDto)
    {
        var roomEntity = _mapper.Map<Room>(roomDto);

        // add amenities if present
        await UpdateRoomAmenities(roomDto.Amenities, roomEntity);

        _repository.Room.AddRoom(roomEntity);
        await _repository.SaveAsync();

        return _mapper.Map<RoomDto>(roomEntity);
    }

    public async Task<RoomDto> GetRoomAsync(int roomId, bool includeAmenity = true,
        bool trackChanges = false)
    {
        var room = await CheckIfRoomExists(roomId, includeAmenity, trackChanges);

        return _mapper.Map<RoomDto>(room);
    }

    public async Task<(IEnumerable<RoomDto>, PageMetadata pageMetadata)> GetRoomsAsync(
        RoomParameters roomParameters, bool trackChanges)
    {
        var roomsWithPageData = await _repository.Room.GetRoomsAsync(roomParameters, trackChanges);

        var roomsDto = _mapper.Map<IEnumerable<RoomDto>>(roomsWithPageData);

        return (roomsDto, pageMetadata: roomsWithPageData.PageMetadata);
    }

    public async Task UpdateRoomAsync(int roomId, RoomUpdateDto roomUpdateDto,
        bool trackChanges = true)
    {
        var room = await CheckIfRoomExists(roomId, includeAmenity: true, trackChanges: trackChanges);

        // update room amenities if present
        await UpdateRoomAmenities(roomUpdateDto.Amenities, room);

        _mapper.Map(roomUpdateDto, room);

        _repository.Room.UpdateModifiedTime(room);

        await _repository.SaveAsync();
    }

    public async Task RemoveRoomAsync(int roomId, bool trackChanges = false)
    {
        var room = await CheckIfRoomExists(roomId, includeAmenity: false, trackChanges);

        _repository.Room.RemoveRoom(room);

        await _repository.SaveAsync();
    }

    private async Task<Room> CheckIfRoomExists(int roomId, bool includeAmenity,
        bool trackChanges = false)
    {
        var room = await _repository.Room.GetRoomAsync(roomId, includeAmenity, trackChanges) ??
            throw new RoomNotFoundException(roomId);

        return room;
    }

    /// <summary>
    /// Add or update room's amenities. Amenities which are already present are excluded
    /// </summary>
    /// <param name="amenitiesIds">List of integer ids of amenities to add</param>
    /// <param name="room">The room to which amenities will be added</param>
    /// <returns></returns>
    private async Task UpdateRoomAmenities(List<int>? amenitiesIds, Room room)
    {
        // check if amenities list is not empty or null
        if (amenitiesIds == null || amenitiesIds.Count == 0)
        {
            return;
        }

        // remove repititions
        var distinctIds = amenitiesIds.Distinct().ToList();

        // find amenities by ids in the list
        List<Amenity> amenitiesInStore = await _repository.Amenity.FindAmenitiesByCondition(distinctIds);
        if (amenitiesInStore == null || amenitiesInStore.Count == 0)
        {
            return;
        }

        // extract ids of amenities found
        var idsFound = amenitiesInStore.Select(amenity => amenity.Id).ToList();

        // get ids of amenities already present in room
        var amenitiesAlreadyPresentInRoom = room.RoomsAmenities
            .Select(room => room.AmenityId)
            .ToList();

        // get ids of amenities not present in room and transform to RoomAmenities
        var amenitiesToAddToRoom = idsFound
            .Where(id => !amenitiesAlreadyPresentInRoom.Contains(id))
            .Select(id => new RoomsAmenities()
            {
                AmenityId = id,
                Room = room
            }).ToList();

        room.RoomsAmenities.AddRange(amenitiesToAddToRoom);
    }
}