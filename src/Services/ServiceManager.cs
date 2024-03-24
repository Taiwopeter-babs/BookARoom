using AutoMapper;
using BookARoom.Interfaces;

namespace BookARoom.Services;

public class ServiceManager : IServiceManager
{
    private readonly Lazy<IRoomService> _roomService;

    public ServiceManager(IRepositoryManager repository, IMapper mapper)
    {
        _roomService = new Lazy<IRoomService>(() => new RoomService(repository, mapper));
    }
    public IRoomService RoomService => _roomService.Value;
}