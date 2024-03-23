using BookARoom.Data;
using BookARoom.Interfaces;

namespace BookARoom.Repository;

public sealed class RepositoryManager : IRepositoryManager
{
    private readonly BookARoomContext _context;
    private readonly Lazy<IAmenityRepository> _amenityRepository;
    private readonly Lazy<IBookingRepository> _bookingRepository;

    private readonly Lazy<IGuestRepository> _guestRepository;

    private readonly Lazy<IRoomRepository> _roomRepository;

    public RepositoryManager(BookARoomContext context)
    {
        _context = context;
        _amenityRepository = new Lazy<IAmenityRepository>(() => new AmenityRepository(context));
        _bookingRepository = new Lazy<IBookingRepository>(() => new BookingRepository(context));
        _guestRepository = new Lazy<IGuestRepository>(() => new GuestRepository(context));
        _roomRepository = new Lazy<IRoomRepository>(() => new RoomRepository(context));
    }

    public IAmenityRepository Amenity => _amenityRepository.Value;

    public IBookingRepository Booking => _bookingRepository.Value;

    public IGuestRepository Guest => _guestRepository.Value;

    public IRoomRepository Room => _roomRepository.Value;

    public async Task SaveAsync() => await _context.SaveChangesAsync();
}