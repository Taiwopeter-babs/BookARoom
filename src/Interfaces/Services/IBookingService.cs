using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IBookingService
{
    Task<BookingDto> AddGuestBookingAsync(int guestId, BookingCreationDto bookingDto);

    Task<BookingDto> GetSingleBookingAsync(int bookingId, bool trackChanges = false);

    Task<BookingDto> GetGuestSingleBookingAsync(int guestId, int bookingId, bool trackChanges = false);

    Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetManyBookingsAsync(
        BookingParameters bookingParams,
        bool trackChanges = false
    );

    Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetGuestManyBookingsAsync(
        int guestId,
        BookingParameters bookingParams,
        bool trackChanges = false
    );


    Task UpdateBookingAsync(int guestId, int bookingId, BookingUpdateDto bookingUpdateDto,
        bool trackChanges = true);

    Task RemoveBookingAsync(int bookingId, bool trackChanges = false);
}