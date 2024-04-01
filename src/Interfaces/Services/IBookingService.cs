using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IBookingService
{
    Task<BookingDto> AddBookingAsync(int guestId, BookingCreationDto bookingDto);

    Task<BookingDto> GetBookingAsync(int guestId, int bookingId, bool trackChanges = false);

    Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetBookingsAsync(
        BookingParameters bookingParams,
        bool trackChanges = false
    );

    // Task<GuestDto> GetGuestBookingsAsync(
    //     int guestId, bool trackChanges = false
    // );

    Task UpdateBookingAsync(int guestId, int bookingId, BookingUpdateDto bookingUpdateDto,
        bool trackChanges = true);

    Task RemoveBookingAsync(int guestId, int bookingId, bool trackChanges = false);
}