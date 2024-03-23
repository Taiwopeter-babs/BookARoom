namespace BookARoom.Interfaces;

public interface IRepositoryManager
{

    IAmenityRepository Amenity { get; }
    IBookingRepository Booking { get; }
    IGuestRepository Guest { get; }
    IRoomRepository Room { get; }

    Task SaveAsync();
}