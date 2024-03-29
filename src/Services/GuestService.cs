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
            return _mapper.Map<GuestDto>(guest);

        var newGuest = _mapper.Map<Guest>(guestDto);

        _repository.Guest.AddGuest(newGuest);

        await _repository.SaveAsync();

        return _mapper.Map<GuestDto>(newGuest);
    }

    public Task<GuestDto> GetGuestAsync(int GuestId, bool includeBookings, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<GuestDto>, PageMetadata pageMetadata)> GetGuestsAsync(GuestParameters GuestParameters, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task RemoveGuestAsync(int GuestId, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task UpdateGuestAsync(int GuestId, GuestUpdateDto GuestForUpdateDto, bool trackChanges = true)
    {
        throw new NotImplementedException();
    }

    private async Task<Guest> CheckGuest(int guestId, bool trackChanges)
    {
        var guest = await _repository.Guest.GetGuestAsync(guestId, trackChanges) ??
            throw new GuestNotFoundException(guestId);

        return guest;
    }
}