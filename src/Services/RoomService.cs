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

    public async Task<RoomDto> AddRoomAsync(RoomForCreationDto roomDto)
    {
        var roomEntity = _mapper.Map<Room>(roomDto);

        _repository.Room.AddRoom(roomEntity);
        await _repository.SaveAsync();

        return _mapper.Map<RoomDto>(roomEntity);
    }

    public async Task<RoomDto> GetRoomAsync(int roomId, bool includeAmenity = false,
        bool trackChanges = false)
    {
        var room = await CheckIfRoomExists(roomId, includeAmenity, trackChanges);

        return _mapper.Map<RoomDto>(room);
    }

    public Task<(IEnumerable<RoomDto>, PageMetadata pageMetadata)> GetRoomsAsync(
        RoomParameters roomParameters, bool trackChanges)
    {
        throw new NotImplementedException();
    }

    private async Task<Room> CheckIfRoomExists(int roomId, bool includeAmenity, bool trackChanges)
    {
        var room = await _repository.Room.GetRoomAsync(roomId, includeAmenity) ??
            throw new RoomNotFoundException(roomId);

        return room;
    }
}