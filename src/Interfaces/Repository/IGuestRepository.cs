using BookARoom.Dto;
using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IGuestRepository
{
    void AddGuest(Guest guest);
    Task<Guest?> GetGuestAsync(int guestId, bool includeBookings = true, bool trackChanges = false);
    Task<Guest?> GetGuestByEmailAsync(string guestEmail, bool trackChanges = false);
    Task<PagedList<Guest>> GetGuestsAsync(GuestParameters guestParameters, bool trackChanges = false);
    void RemoveGuest(Guest guest);

    void UpdateModifiedTime(Guest guest);

    // Task<BookingDto> CreateBooking(int guestId, BookingCreationDto bookingCreationDto);
}