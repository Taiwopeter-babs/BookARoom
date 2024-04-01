using AutoMapper;
using BookARoom.Interfaces;
using BookARoom.Dto;
using BookARoom.Utilities;
using BookARoom.Models;
using BookARoom.Exceptions;

namespace BookARoom.Services;

public class GuestService : IGuestService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;


    public GuestService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<GuestDto> AddGuestAsync(GuestCreationDto guestDto)
    {
        // return a guest if it already exists
        var guest = await _repository.Guest
            .GetGuestByEmailAsync(guestDto.Email!, trackChanges: false);

        if (guest != null)
        {
            return _mapper.Map<GuestDto>(guest);
        }

        var newGuest = _mapper.Map<Guest>(guestDto);

        _repository.Guest.AddGuest(newGuest);

        await _repository.SaveAsync();

        var guestDtoReturn = _mapper.Map<GuestDto>(newGuest);
        guestDtoReturn.NewGuest = true;

        return guestDtoReturn;
    }

    public async Task<GuestDto> GetGuestAsync(int guestId, bool trackChanges = false)
    {
        var guest = await CheckGuest(guestId, trackChanges: trackChanges);

        return _mapper.Map<GuestDto>(guest);
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
    }

    public async Task UpdateGuestAsync(int guestId, GuestUpdateDto guestUpdateDto,
        bool trackChanges = true)
    {
        var guest = await CheckGuest(guestId, trackChanges);

        _mapper.Map(guestUpdateDto, guest);

        _repository.Guest.UpdateModifiedTime(guest);

        await _repository.SaveAsync();
    }

    private async Task<Guest> CheckGuest(int guestId, bool trackChanges)
    {
        var guest = await _repository.Guest.GetGuestAsync(guestId, trackChanges: trackChanges) ??
            throw new GuestNotFoundException(guestId);

        return guest;
    }
}