using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Redis;
using BookARoom.Utilities;

namespace BookARoom.Services;

internal sealed class RoomService : IRoomService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IRedisService _redisService;

    public RoomService(IRepositoryManager repository, IMapper mapper, IRedisService redisService)
    {
        _repository = repository;
        _mapper = mapper;
        _redisService = redisService;
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
        var room = await CheckRoom(roomId, includeAmenity, trackChanges);

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
        var room = await CheckRoom(roomId, includeAmenity: true, trackChanges: trackChanges);

        // update room amenities if present
        await UpdateRoomAmenities(roomUpdateDto.Amenities, room);

        _mapper.Map(roomUpdateDto, room);

        if (room.NumberAvailable > 0)
            room.IsAvailable = true;

        _repository.Room.UpdateModifiedTime(room);

        await _repository.SaveAsync();

        // Update in cache
        string stringId = roomId.ToString();
        await _redisService.SaveObjectAsync(stringId, room);
    }

    public async Task RemoveRoomAsync(int roomId, bool trackChanges = false)
    {
        var room = await CheckRoom(roomId, includeAmenity: false, trackChanges);

        _repository.Room.RemoveRoom(room);

        await _repository.SaveAsync();

        // remove from cache
        string stringId = roomId.ToString();
        await _redisService.DeleteAsync<Room>(stringId);
    }

    private async Task<Room> CheckRoom(int roomId, bool includeAmenity,
        bool trackChanges = false)
    {
        RoomDto? room;
        Room? roomEntity;

        string stringId = roomId.ToString();

        room = await _redisService.GetValueAsync<RoomDto>(stringId);

        // Cache miss: Get object from database and save in cache
        if (room == null || string.IsNullOrEmpty(room?.ToString()))
        {
            roomEntity = await _repository.Room.GetRoomAsync(roomId, includeAmenity, trackChanges) ??
                throw new RoomNotFoundException(roomId);

            // save in redis cache
            room = MapRoomToDto(roomEntity);
            await _redisService.SaveObjectAsync(stringId, room);
        }
        else
        {
            roomEntity = MapToRoom(room);
        }

        return roomEntity;
    }

    /// <summary>
    /// Maps a room to the Dto type
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    private RoomDto MapRoomToDto(Room room) => _mapper.Map<RoomDto>(room);

    /// <summary>
    /// Maps from RoomDto to the Room object
    /// </summary>
    /// <param name="room"></param>
    /// <returns></returns>
    private Room MapToRoom(RoomDto room) => _mapper.Map<Room>(room);

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
            return;

        // remove repititions
        var distinctIds = amenitiesIds.Distinct().ToList();

        // find amenities by ids in the list
        List<Amenity> amenitiesInStore = await _repository.Amenity.FindAmenitiesByCondition(distinctIds);
        if (amenitiesInStore == null || amenitiesInStore.Count == 0)
            return;

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