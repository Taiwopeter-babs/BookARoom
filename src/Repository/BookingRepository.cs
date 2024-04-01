
using BookARoom.Data;
using BookARoom.Extensions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Repository;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(BookARoomContext context) : base(context)
    {
    }

    public void AddBooking(Booking booking) => Create(booking);

    public async Task<PagedList<Booking>> GetBookingsAsync(BookingParameters bookingParams,
        bool trackChanges = false)
    {
        var bookingDate = bookingParams.BookingDate.GetUtcDate();
        var checkinDate = bookingParams.CheckinDate.GetUtcDate();
        var checkoutDate = bookingParams.CheckoutDate.GetUtcDate();

        var bookings = await FindAll(trackChanges)
            .FilterByBookingDate(bookingDate)
            .FilterByCheckinDate(checkinDate)
            .FilterByCheckoutDate(checkoutDate)
            .OrderBy(booking => booking.BookingDate)
            .ToListAsync();

        var bookingsCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Booking>.ToPagedList(bookings, bookingsCount,
            bookingParams.PageNumber, bookingParams.PageSize);
    }

    public async Task<Booking?> GetBookingAsync(int bookingId, bool includeGuest = true,
        bool trackChanges = false)
    {
        return await FindByCondition(booking => booking.Id == bookingId, trackChanges)
            .IncludeGuest(includeGuest)
            .SingleOrDefaultAsync();
    }

    public void RemoveBooking(Booking booking) => Delete(booking);

    public void UpdateModifiedTime(Booking booking) => UpdateModifiedTime(booking);
}