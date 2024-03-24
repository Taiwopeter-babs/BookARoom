
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;

namespace BookARoom.Repository;

public class AmenityRepository : RepositoryBase<Amenity>, IAmenityRepository
{
    public AmenityRepository(BookARoomContext context) : base(context)
    {
    }
}