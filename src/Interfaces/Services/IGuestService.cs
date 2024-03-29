using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IGuestService
{
    Task<GuestDto> AddGuestAsync(GuestCreationDto guestDto);

    Task<GuestDto> GetGuestAsync(int GuestId, bool includeBookings,
        bool trackChanges = false);

    Task<(IEnumerable<GuestDto>, PageMetadata pageMetadata)> GetGuestsAsync(
        GuestParameters guestParameters, bool trackChanges = false);

    Task UpdateGuestAsync(int GuestId, GuestUpdateDto GuestForUpdateDto,
        bool trackChanges = true);

    Task RemoveGuestAsync(int GuestId, bool trackChanges = false);

    // Task BookGuest(GuestBookingDto GuestToBook);
}