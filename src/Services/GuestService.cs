using AutoMapper;
using BookARoom.Interfaces;
using BookARoom.Dto;
using BookARoom.Utilities;
using BookARoom.Models;
using BookARoom.Exceptions;
using BookARoom.Redis;

namespace BookARoom.Services;

public class GuestService : IGuestService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IRedisService _redisService;


    public GuestService(IRepositoryManager repository, IMapper mapper, IRedisService redisService)
    {
        _repository = repository;
        _mapper = mapper;
        _redisService = redisService;
    }

    public async Task<GuestDto> AddGuestAsync(GuestCreationDto guestDto)
    {
        // return a guest if it already exists
        var guest = await _repository.Guest
            .GetGuestByEmailAsync(guestDto.Email!, trackChanges: false);

        if (guest != null)
        {
            return MapToGuestDto(guest);
        }

        var newGuest = _mapper.Map<Guest>(guestDto);

        _repository.Guest.AddGuest(newGuest);

        await _repository.SaveAsync();

        var guestDtoReturn = MapToGuestDto(newGuest);

        guestDtoReturn.NewGuest = true;

        return guestDtoReturn;
    }

    public async Task<GuestDto> GetGuestAsync(int guestId, bool trackChanges = false)
    {
        var guest = await CheckGuest(guestId, trackChanges: trackChanges);

        return MapToGuestDto(guest);
    }

    public async Task<(IEnumerable<GuestDto>, PageMetadata pageMetadata)> GetGuestsAsync(
        GuestParameters guestParams, bool trackChanges = false)
    {
        var guestsWithPageData = await _repository.Guest.GetGuestsAsync(guestParams, trackChanges);

        var guestsDtos = _mapper.Map<IEnumerable<GuestDto>>(guestsWithPageData);

        return (guestsDtos, pageMetadata: guestsWithPageData.PageMetadata);
    }

    public async Task RemoveGuestAsync(int guestId, bool trackChanges = false)
    {
        var guest = await CheckGuest(guestId, trackChanges);

        _repository.Guest.RemoveGuest(guest);

        await _repository.SaveAsync();

        // remove from cache
        string stringId = guestId.ToString();
        await _redisService.DeleteAsync<Guest>(stringId);
    }

    public async Task UpdateGuestAsync(int guestId, GuestUpdateDto guestUpdateDto,
        bool trackChanges = true)
    {
        var guest = await CheckGuest(guestId, trackChanges);

        _mapper.Map(guestUpdateDto, guest);

        _repository.Guest.UpdateModifiedTime(guest);

        await _repository.SaveAsync();

        // Update in cache
        string stringId = guestId.ToString();
        await _redisService.SaveObjectAsync(stringId, guest);
    }

    public async Task<Guest> CheckGuest(int guestId, bool trackChanges)
    {
        Guest? guestEntity;
        GuestDto? guest;

        string stringId = guestId.ToString();

        guest = await _redisService.GetValueAsync<GuestDto>(stringId);

        // Cache miss: Get object from database and save in cache
        if (guest == null || string.IsNullOrEmpty(guest?.ToString()))
        {
            guestEntity = await _repository.Guest.GetGuestAsync(guestId, trackChanges) ??
                 throw new GuestNotFoundException(guestId);

            // save in redis cache
            guest = MapToGuestDto(guestEntity);
            await _redisService.SaveObjectAsync(stringId, guest);
        }
        else
        {
            // Map to Guest type
            guestEntity = MapToGuest(guest);
        }

        return guestEntity;
    }

    private Guest MapToGuest(GuestDto guest) => _mapper.Map<Guest>(guest);

    private GuestDto MapToGuestDto(Guest guest) => _mapper.Map<GuestDto>(guest);
}