using BookARoom.Models;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Extensions;

public static class GuestRepositoryExtension
{
    /// <summary>
    /// Extension method to include Bookings in the query.
    /// </summary>
    /// <param name="guest"></param>
    /// <returns></returns>
    public static IQueryable<Guest> IncludeBookings(this IQueryable<Guest> guest,
        bool includeBookings)
    {
        return includeBookings ?
            guest.Include(guest => guest.Bookings).AsSplitQuery() :
            guest;

    }

    public static IQueryable<Guest> FilterByCountry(this IQueryable<Guest> guests,
        string? country)
    {
        return string.IsNullOrWhiteSpace(country) ?
            guests : guests.Where(guest => guest.Country.Equals(country));
    }

    public static IQueryable<Guest> FilterByState(this IQueryable<Guest> guests,
        string? state)
    {
        return string.IsNullOrWhiteSpace(state) ?
            guests : guests.Where(guest => guest.State.Equals(state));
    }

    public static IQueryable<Guest> FilterByNumberOfBookings(this IQueryable<Guest> guests,
        int minBookings, int maxBookings)
    {
        return guests.Where(guest =>
            guest.NumberOfBookings >= minBookings && guest.NumberOfBookings <= maxBookings);
    }

    public static IQueryable<Guest> FilterByGuestsCreationDate(this IQueryable<Guest> guests,
        DateTime minDate, DateTime maxDate)
    {
        return guests.Where(guest =>
            (guest.CreatedAt >= minDate) && (guest.CreatedAt <= maxDate));
    }

    public static IQueryable<Guest> FilterByLastBookingDate(this IQueryable<Guest> guests,
        DateTime lastBookingDate)
    {
        // Guests who have not made any bookings will have lastBookingDate field as null
        return guests
            .Where(guest => guest.LastBookingDate >= lastBookingDate);
    }
}
