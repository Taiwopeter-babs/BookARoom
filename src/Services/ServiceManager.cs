using AutoMapper;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Redis;

namespace BookARoom.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IRoomService> _roomService;
    private readonly Lazy<IAmenityService> _amenityService;
    private readonly Lazy<IGuestService> _guestService;
    private readonly Lazy<IBookingService> _bookingService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper, IRedisService redisService)
    {
        _roomService = new Lazy<IRoomService>(() => new RoomService(repository, mapper, redisService));
        _amenityService = new Lazy<IAmenityService>(() => new AmenityService(repository, mapper, redisService));
        _guestService = new Lazy<IGuestService>(() => new GuestService(repository, mapper, redisService));
        _bookingService = new Lazy<IBookingService>(() => new BookingService(repository, mapper, redisService));
    }
    public IRoomService RoomService => _roomService.Value;
    public IAmenityService AmenityService => _amenityService.Value;
    public IGuestService GuestService => _guestService.Value;

    public IBookingService BookingService => _bookingService.Value;
}