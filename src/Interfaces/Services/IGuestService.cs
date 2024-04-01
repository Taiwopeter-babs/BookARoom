using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IGuestService
{
    Task<GuestDto> AddGuestAsync(GuestCreationDto guestDto);

    Task<GuestDto> GetGuestAsync(int guestId, bool trackChanges);

    Task<(IEnumerable<GuestDto>, PageMetadata pageMetadata)> GetGuestsAsync(
        GuestParameters guestParams, bool trackChanges = false);

    Task UpdateGuestAsync(int guestId, GuestUpdateDto guestForUpdateDto,
        bool trackChanges = true);

    Task RemoveGuestAsync(int guestId, bool trackChanges = false);

    // Task BookGuest(GuestBookingDto GuestToBook);
}