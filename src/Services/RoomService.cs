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

    public async Task UpdateRoomAsync(int roomId, RoomForUpdateDto roomForUpdateDto,
        bool trackChanges = true)
    {
        var room = await CheckIfRoomExists(roomId, includeAmenity: false, trackChanges: trackChanges);

        _mapper.Map(roomForUpdateDto, room);
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
}