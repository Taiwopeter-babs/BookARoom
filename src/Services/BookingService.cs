using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Extensions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Redis;
using BookARoom.Utilities;

namespace BookARoom.Services;

public class BookingService : IBookingService
{
    private IRepositoryManager _repository;
    private IMapper _mapper;
    private readonly IRedisService _redisService;

    public BookingService(IRepositoryManager repository, IMapper mapper, IRedisService redisService)
    {
        _repository = repository;
        _mapper = mapper;
        _redisService = redisService;
    }


    public async Task<BookingDto> AddGuestBookingAsync(int guestId, BookingCreationDto bookingDto)
    {
        var guest = await CheckGuest(guestId, trackChanges: true);

        Booking bookingEntity = _mapper.Map<Booking>(bookingDto);

        // book rooms that are available
        await BookRoomsForGuest(bookingEntity, guest, bookingDto.Rooms);

        guest.UpdateGuestFields(bookingEntity);

        _repository.Booking.AddBooking(bookingEntity);

        await _repository.SaveAsync();

        return MapBooking(bookingEntity);
    }

    public async Task<BookingDto> GetSingleBookingAsync(int bookingId,
        bool trackChanges = false)
    {
        var booking = await CheckBooking(bookingId, includeRooms: true);

        return booking;
    }

    public async Task<BookingDto> GetGuestSingleBookingAsync(int guestId, int bookingId,
        bool trackChanges = false)
    {
        await CheckGuest(guestId);

        var booking = await _repository.Booking
            .GetGuestSingleBookingAsync(guestId, bookingId, trackChanges: trackChanges);

        if (booking == null)
            throw new BookingNotAvailableForGuestException(guestId, bookingId);

        var bookingEntity = MapBooking(booking);

        return bookingEntity;
    }

    public async Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetManyBookingsAsync(
        BookingParameters bookingParams, bool trackChanges = false)
    {
        var bookingsWithPageData = await _repository.Booking.GetBookingsAsync(bookingParams, trackChanges);

        var bookingsDtos = _mapper.Map<IEnumerable<BookingDto>>(bookingsWithPageData);

        return (bookingsDtos, pageMetadata: bookingsWithPageData.PageMetadata);
    }

    public async Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetGuestManyBookingsAsync(
        int guestId, BookingParameters bookingParams, bool trackChanges = false)
    {
        var bookingsWithPageData = await _repository.Booking
            .GetGuestBookingsAsync(guestId, bookingParams, trackChanges);

        var bookingsDtos = _mapper.Map<IEnumerable<BookingDto>>(bookingsWithPageData);

        return (bookingsDtos, pageMetadata: bookingsWithPageData.PageMetadata);
    }

    public async Task RemoveBookingAsync(int bookingId, bool trackChanges = true)
    {
        var booking = await _repository.Booking.GetJoinDataAsync(bookingId);
        if (booking == null)
            throw new BookingNotFoundException(bookingId);

        // find all the rooms connected to booking and update
        await UpdateRoomsForBooking(booking);

        _repository.Booking.RemoveBooking(booking);

        await _repository.SaveAsync();

        // remove from cache
        string stringId = bookingId.ToString();
        await _redisService.DeleteAsync<BookingDto>(stringId);
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
            RoomsBookings? bookingData = booking.RoomsBookings.Find(rb => room.Id.Equals(rb.RoomId));

            if (bookingData == null)
                continue;

            room.NumberAvailable += bookingData.NumberOfRooms;
        }
    }

    /// <summary>
    /// finds and books rooms for a guest
    /// </summary>
    /// <param name="bookingEntity">The booking</param>
    /// <param name="roomsToBook">A list of <code>RoomBookingDto</code> rooms</param>
    /// <returns></returns>
    private async Task BookRoomsForGuest(Booking bookingEntity, Guest guest,
        List<RoomBookingDto>? roomsToBook)
    {
        if (roomsToBook == null || roomsToBook.Count == 0)
            throw new UnsuccefulBookingGuestException(guest.Id);

        List<int> roomsId = roomsToBook.Select(roomToBook => roomToBook.RoomId).ToList();

        List<Room> availableRooms = await _repository.Room.FindAvailableRooms(roomsId, trackChanges: true);

        if (availableRooms.Count == 0)
            throw new UnsuccefulBookingGuestException(guest.Id);

        // Add available rooms to booking entity
        bookingEntity.AddAvailableRoomsForBooking(guest, availableRooms, roomsToBook);

    }


    private async Task<Guest> CheckGuest(int guestId, bool trackChanges = false)
    {
        Guest guest = await _repository.Guest.GetGuestAsync(guestId, trackChanges) ??
                 throw new GuestNotFoundException(guestId);

        return guest;
    }

    private async Task<BookingDto> CheckBooking(int bookingId, bool includeGuest = true,
        bool includeRooms = true, bool trackChanges = false)
    {
        BookingDto? booking;
        string stringId = bookingId.ToString();

        booking = await _redisService.GetValueAsync<BookingDto>(stringId);

        // Cache miss: Get object from database and save in cache
        if (booking == null || string.IsNullOrEmpty(booking?.ToString()))
        {
            var bookingObj = await _repository.Booking
                .GetSingleBookingAsync(bookingId, includeGuest, includeRooms, trackChanges) ??
            throw new BookingNotFoundException(bookingId);

            // save in redis cache
            booking = MapBooking(bookingObj);
            await _redisService.SaveObjectAsync(stringId, booking);
        }

        return booking;
    }

    /// <summary>
    /// Maps the booking Booking type to BookingDto type
    /// </summary>
    /// <param name="booking"></param>
    /// <returns></returns>
    private BookingDto MapBooking(Booking booking)
    {
        var bookingEntity = _mapper.Map<BookingDto>(booking);

        if (booking.Rooms != null && booking.Rooms.Count > 0)
        {
            var rooms = _mapper.Map<List<RoomDto>>(booking.Rooms);
            bookingEntity.RoomsBooked = rooms ?? null;
        }

        return bookingEntity;

    }
}