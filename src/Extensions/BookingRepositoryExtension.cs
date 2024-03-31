using BookARoom.Models;

namespace BookARoom;

public static class BookingRepositoryExtension
{
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
