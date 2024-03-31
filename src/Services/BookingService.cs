using AutoMapper;
using BookARoom.Dto;
using BookARoom.Interfaces;
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
        throw new NotImplementedException();
    }

    public async Task<BookingDto> GetBookingAsync(int guestId, int bookingId,
        bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public async Task<(IEnumerable<BookingDto>, PageMetadata pageMetadata)> GetBookingsAsync(
        int guestId, BookingParameters bookingParams, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public async Task RemoveBookingAsync(int guestId, int bookingId, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateBookingAsync(int guestId, int bookingId,
        BookingUpdateDto bookingUpdateDto, bool trackChanges = true)
    {
        throw new NotImplementedException();
    }
}