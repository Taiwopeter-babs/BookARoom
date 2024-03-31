using AutoMapper;
using BookARoom.Interfaces;

namespace BookARoom.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IRoomService> _roomService;
    private readonly Lazy<IAmenityService> _amenityService;
    private readonly Lazy<IGuestService> _guestService;
    private readonly Lazy<IBookingService> _bookingService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _roomService = new Lazy<IRoomService>(() => new RoomService(repository, mapper));
        _amenityService = new Lazy<IAmenityService>(() => new AmenityService(repository, mapper));
        _guestService = new Lazy<IGuestService>(() => new GuestService(repository, mapper));
        _bookingService = new Lazy<IBookingService>(() => new BookingService(repository, mapper));
    }
    public IRoomService RoomService => _roomService.Value;
    public IAmenityService AmenityService => _amenityService.Value;
    public IGuestService GuestService => _guestService.Value;

    public IBookingService BookingService => _bookingService.Value;
}