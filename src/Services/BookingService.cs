using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Extensions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Services;

public class BookingService : IBookingService
{
    private IRepositoryManager _repository;
    private IMapper _mapper;

    public BookingService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }


    public async Task<BookingDto> AddBookingAsync(int guestId, BookingCreationDto bookingDto)
    {
        var guest = await CheckGuest(guestId, trackChanges: true);

        Booking bookingEntity = _mapper.Map<Booking>(bookingDto);

        // book rooms that are available
        await BookRoomsForGuest(guest, bookingEntity, bookingDto.Rooms);

        guest.UpdateGuestFields(bookingEntity);

        _repository.Booking.AddBooking(bookingEntity);

        await _repository.SaveAsync();

        return _mapper.Map<BookingDto>(bookingEntity);
    }

    public async Task<BookingDto> GetBookingAsync(int guestId, int bookingId,
        bool trackChanges = false)
    {
        var guest = await CheckGuest(guestId);

        var booking = await CheckBooking(bookingId, includeRooms: true);


        if (booking.GuestId != guest.Id)
            throw new BookingNotAvailableForGuestException(guestId, bookingId);

        var bookingEntity = _mapper.Map<BookingDto>(booking);

        if (booking.Rooms != null && booking.Rooms.Count > 0)
        {
            var rooms = _mapper.Map<List<RoomDto>>(booking.Rooms);
            bookingEntity.RoomsBooked = rooms ?? null;
        }

        return bookingEntity;
    }

    public async Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetBookingsAsync(
        BookingParameters bookingParams, bool trackChanges = false)
    {
        var bookingsWithPageData = await _repository.Booking.GetBookingsAsync(bookingParams, trackChanges);

        var bookingsDtos = _mapper.Map<IEnumerable<BookingDto>>(bookingsWithPageData);

        return (bookingsDtos, pageMetadata: bookingsWithPageData.PageMetadata);
    }

    public async Task RemoveBookingAsync(int guestId, int bookingId, bool trackChanges = true)
    {
        await CheckGuest(guestId, trackChanges);

        // var booking = await CheckBooking(bookingId, includeGuest: false, trackChanges: trackChanges);
        var booking = await _repository.Booking.GetJoinDataAsync(bookingId);
        if (booking == null)
            throw new BookingNotFoundException(bookingId);

        Console.WriteLine(booking.RoomsBookings.Count);

        // find all the rooms connected to booking and update
        await UpdateRoomsForBooking(booking);

        _repository.Booking.RemoveBooking(booking);

        await _repository.SaveAsync();
    }

    public async Task UpdateBookingAsync(int guestId, int bookingId,
        BookingUpdateDto bookingUpdateDto, bool trackChanges = true)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Update the rooms booked by adding the NumberOfRooms to the room
    /// NumberAvailable field. This ensures that after a booking has been deleted
    /// or expired, the rooms booked will have their amount available increased.
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    private async Task UpdateRoomsForBooking(Booking booking)
    {
        if (booking.RoomsBookings == null || booking.RoomsBookings!.Count == 0)
            return;


        List<int> bookedRoomsIds = booking.RoomsBookings
            .Select(roomBooked => roomBooked.RoomId)
            .ToList();

        List<Room> bookedRooms = await _repository.Room
            .FindAvailableRooms(bookedRoomsIds, trackChanges: true);


        foreach (Room room in bookedRooms)
        {
            // match rooms and update NumberAvailable field
            RoomsBookings? data = booking.RoomsBookings.Find(rb => room.Id.Equals(rb.RoomId));

            if (data == null)
                continue;

            Console.WriteLine(room.Name);
            Console.WriteLine($"{room.NumberAvailable} before");

            room.NumberAvailable += data.NumberOfRooms;

            Console.WriteLine($"{room.NumberAvailable} after");
        }

        // await _repository.SaveAsync();
    }

    /// <summary>
    /// finds and books rooms for a guest
    /// </summary>
    /// <param name="bookingEntity">The booking</param>
    /// <param name="roomsToBook">A list of <code>RoomBookingDto</code> rooms</param>
    /// <returns></returns>
    private async Task BookRoomsForGuest(Guest guest, Booking bookingEntity,
        List<RoomBookingDto>? roomsToBook)
    {
        if (roomsToBook == null || roomsToBook.Count == 0)
            throw new UnsuccefulBookingGuestException(guest.Id);

        var roomsId = roomsToBook.Select(roomToBook => roomToBook.RoomId).ToList();

        List<Room> availableRooms = await _repository.Room.FindAvailableRooms(roomsId, trackChanges: true);

        if (availableRooms.Count == 0)
            throw new UnsuccefulBookingGuestException(guest.Id);

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

            int totalGuestsAccomodated = room.MaximumOccupancy * roomDto.NumberRooms;
            bool canBeBooked = totalGuestsAccomodated >= roomDto.NumberGuests;
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
        // bookingEntity.Guest = guest;
    }

    private async Task<Guest> CheckGuest(int guestId, bool trackChanges = false)
    {
        var guest = await _repository.Guest.GetGuestAsync(guestId, trackChanges) ??
            throw new GuestNotFoundException(guestId);

        return guest;
    }

    private async Task<Booking> CheckBooking(int bookingId, bool includeGuest = true,
        bool includeRooms = true, bool trackChanges = false)
    {
        var booking = await _repository.Booking
            .GetBookingAsync(bookingId, includeGuest, includeRooms, trackChanges) ??
            throw new BookingNotFoundException(bookingId);

        return booking;
    }
}