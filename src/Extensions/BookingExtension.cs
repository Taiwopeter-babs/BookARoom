using BookARoom.Dto;
using BookARoom.Exceptions;
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
        // Console.WriteLine(includeGuest);
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
        return booking.Include(booking => booking.RoomsBookings)
            .AsSplitQuery();
    }

    public static IQueryable<Booking> FilterByGuestId(this IQueryable<Booking> bookings,
       int guestId)
    {
        return guestId > 0 ?
            bookings.Where(booking => booking.GuestId >= guestId) :
            bookings;
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

    /// <summary>
    /// Adds the available rooms found from the list of provided rooms to book to
    /// the booking entity.
    /// </summary>
    /// <param name="bookingEntity">Booking entity to which the rooms booked will be added to</param>
    /// <param name="guest">Guest naking the booking</param>
    /// <param name="availableRooms">List of rooms available for booking</param>
    /// <param name="roomsToBook">List of rooms from which avialable rooms will be picked</param>
    /// <exception cref="UnsuccefulBookingGuestException">An exception for any unsuccessful booking</exception>
    /// <exception cref="RoomMaximumOccupancyExceededException">An exception for exceeded rooms' limit</exception>
    public static void AddAvailableRoomsForBooking(
        this Booking bookingEntity, Guest guest,
        List<Room> availableRooms,
        List<RoomBookingDto> roomsToBook)
    {

        List<RoomsBookings> roomsBookings = new();

        // map room
        foreach (Room room in availableRooms)
        {
            // find the corresponding RoomBookingDto object
            RoomBookingDto? roomDto = roomsToBook.Find(roomDto => roomDto.RoomId.Equals(room.Id));
            if (roomDto == null)
                throw new UnsuccefulBookingGuestException(guest.Id);

            // validate number available and the guests number
            if (
                room.IsAvailable == false ||
                room.NumberAvailable < roomDto.NumberRooms
            )
                throw new UnsuccefulBookingGuestException(guest.Id);

            int validGuests = room.MaximumOccupancy * roomDto.NumberRooms;
            bool canBeBooked = validGuests >= roomDto.NumberGuests;

            int validRoomAmount = (int)Math.Ceiling(roomDto.NumberGuests / (decimal)room.MaximumOccupancy);

            if (!canBeBooked)
                throw new RoomMaximumOccupancyExceededException(room.Id, room.Name, validRoomAmount);

            // update room fields
            room.NumberAvailable -= roomDto.NumberRooms;
            room.IsAvailable = room.NumberAvailable > 0;
            room.Bookings.Add(bookingEntity);

            // skip duplicate entries
            if (roomsBookings.Find(rb => rb.RoomId.Equals(room.Id)) != null)
                continue;

            roomsBookings.Add(new RoomsBookings
            {
                Booking = bookingEntity,
                RoomId = room.Id,
                NumberOfRooms = roomDto.NumberRooms
            });
        }

        bookingEntity.RoomsBookings.AddRange(roomsBookings);
    }
}
