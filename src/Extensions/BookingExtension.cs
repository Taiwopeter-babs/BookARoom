using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom;

public static class BookingRepositoryExtension
{

    /// <summary>
    /// An extension method to include the Guest in the query result
    /// </summary>
    /// <param name="booking"></param>
    /// <param name="includeGuest"></param>
    /// <returns></returns>
    public static IQueryable<Booking> IncludeGuest(this IQueryable<Booking> booking,
        bool includeGuest)
    {
        return includeGuest ?
            booking.Include(booking => booking.Guest).AsSplitQuery() :
            booking;

    }

    public static IQueryable<Booking> IncludeRooms(this IQueryable<Booking> booking,
        bool includeRooms)
    {
        return includeRooms ?
            booking.Include(booking => booking.Rooms).AsSplitQuery() :
            booking;

    }

    public static IQueryable<Booking> GetJoinData(this IQueryable<Booking> booking)
    {
        // SELECT b.Id as bookingId, r.Id as roomId, rb.NumberOfRooms as NumberOfRoomsBooked
        // FROM bookings AS b
        // INNER JOIN b.RoomsBookings AS rb
        // ON b.Id = rb.BookingId
        // INNER JOIN rooms AS r
        // ON rb.RoomId = r.Id
        // WHERE b.Id = value AND r.Id = val2
        return booking.Include(booking => booking.RoomsBookings)
            .AsSplitQuery();

    }

    public static IQueryable<Booking> FilterByBookingDate(this IQueryable<Booking> bookings,
       DateTime bookingDate)
    {
        return bookings.Where(booking => booking.BookingDate >= bookingDate);
    }

    public static IQueryable<Booking> FilterByCheckinDate(this IQueryable<Booking> bookings,
       DateTime checkinDate)
    {
        return bookings.Where(booking => booking.CheckinDate >= checkinDate);
    }

    public static IQueryable<Booking> FilterByCheckoutDate(this IQueryable<Booking> bookings,
      DateTime checkoutDate)
    {
        return bookings.Where(booking => booking.CheckoutDate >= checkoutDate);
    }
}
