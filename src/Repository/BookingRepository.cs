
using System.Linq.Expressions;
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
        var (bookings, bookingsCount) = await GetManyBookings(bookingParams, trackChanges);

        return PagedList<Booking>.ToPagedList(bookings, bookingsCount,
            bookingParams.PageNumber, bookingParams.PageSize);
    }

    public async Task<PagedList<Booking>> GetGuestBookingsAsync(
        int guestId, BookingParameters bookingParams, bool trackChanges = false)
    {

        var (bookings, bookingsCount) = await GetManyBookings(bookingParams, trackChanges, guestId);

        return PagedList<Booking>.ToPagedList(bookings, bookingsCount,
            bookingParams.PageNumber, bookingParams.PageSize);
    }

    /// <summary>
    /// Gets a single booking entity
    /// </summary>
    /// <param name="bookingId"></param>
    /// <param name="includeGuest"></param>
    /// <param name="includeRooms"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Booking?> GetSingleBookingAsync(
        int bookingId, bool includeGuest = true,
        bool includeRooms = true, bool trackChanges = false)
    {
        return await GetSingleBooking(booking => booking.Id == bookingId, trackChanges: trackChanges)
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Gets a single booking connected to a guest
    /// </summary>
    /// <param name="guestId"></param>
    /// <param name="bookingId"></param>
    /// <param name="includeGuest"></param>
    /// <param name="includeRooms"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Booking?> GetGuestSingleBookingAsync(
        int guestId, int bookingId, bool includeGuest = true,
        bool includeRooms = true, bool trackChanges = false)
    {
        return await GetSingleBooking(booking =>
            booking.Id.Equals(bookingId) && booking.GuestId.Equals(guestId),
            trackChanges: trackChanges
            )
            .SingleOrDefaultAsync();
    }

    public void RemoveBooking(Booking booking) => Delete(booking);

    public void UpdateModifiedTime(Booking booking) => UpdateModifiedTime(booking);


    public async Task<Booking?> GetJoinDataAsync(int bookingId, bool trackChanges = true)
    {
        return await FindByCondition(booking => booking.Id == bookingId, trackChanges)
            .GetJoinData()
            .SingleOrDefaultAsync();
    }

    /// <summary>
    /// Builds the IQueryable for getting a booking by a condition
    /// </summary>
    /// <param name="expression"></param>
    /// <param name="includeGuest"></param>
    /// <param name="includeRooms"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    private IQueryable<Booking> GetSingleBooking(
        Expression<Func<Booking, bool>> expression,
        bool includeGuest = true,
        bool includeRooms = true, bool trackChanges = false)
    {
        return FindByCondition(expression, trackChanges)
            .IncludeGuest(includeGuest)
            .IncludeRooms(includeRooms);
    }

    /// <summary>
    /// Builds the IQueryable for geeting multiple bookings with filters
    /// </summary>
    /// <param name="bookingParams"></param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    private async Task<(IQueryable<Booking>, int count)> GetManyBookings(
        BookingParameters bookingParams, bool trackChanges = false, int guestId = 0
        )
    {
        var bookingDate = bookingParams.BookingDate.GetUtcDate();
        var checkinDate = bookingParams.CheckinDate.GetUtcDate();
        var checkoutDate = bookingParams.CheckoutDate.GetUtcDate();

        var bookings = FindAll(trackChanges)
            .FilterByGuestId(guestId)
            .FilterByBookingDate(bookingDate)
            .FilterByCheckinDate(checkinDate)
            .FilterByCheckoutDate(checkoutDate)
            .OrderBy(booking => bookingDate);

        var bookingsCount = await FindAll(trackChanges).CountAsync();

        return (bookings, bookingsCount);
    }


}