
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;

namespace BookARoom.Repository;

public class BookingRepository : RepositoryBase<Booking>, IBookingRepository
{
    public BookingRepository(BookARoomContext context) : base(context)
    {
    }
}