
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;

namespace BookARoom.Repository;

public class GuestRepository : RepositoryBase<Guest>, IGuestRepository
{
    public GuestRepository(BookARoomContext context) : base(context)
    {
    }
}