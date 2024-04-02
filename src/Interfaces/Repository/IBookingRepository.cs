using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IBookingRepository
{
    void AddBooking(Booking booking);
    Task<Booking?> GetBookingAsync(int bookingId, bool includeGuest = true,
     bool includeRooms = true, bool trackChanges = false);
    Task<PagedList<Booking>> GetBookingsAsync(BookingParameters bookingParams,
        bool trackChanges = false);
    void RemoveBooking(Booking booking);

    void UpdateModifiedTime(Booking booking);

    Task<Booking?> GetJoinDataAsync(int bookingId, bool trackChanges = true);
}