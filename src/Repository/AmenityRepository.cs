
using BookARoom.Data;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BookARoom.Repository;

public class AmenityRepository : RepositoryBase<Amenity>, IAmenityRepository
{
    public AmenityRepository(BookARoomContext context) : base(context)
    {
    }

    public void AddAmenity(Amenity amenity) => Create(amenity);

    /// <summary>
    /// Gets a Room.
    /// </summary>
    /// <param name="roomId"></param>
    /// <param name="includeAmenity">Set to false by default to exclude relationship entities</param>
    /// <param name="trackChanges"></param>
    /// <returns></returns>
    public async Task<Amenity?> GetAmenityAsync(int amenityId, bool trackChanges = false)
    {
        return await FindByCondition(am => am.Id == amenityId, trackChanges)
            .SingleOrDefaultAsync();
    }

    public async Task<PagedList<Amenity>> GetAmenitiesAsync(AmenityParameters amenityParams,
        bool trackChanges)
    {
        var amenities = await FindAll(trackChanges)
            .OrderBy(am => am.Name)
            .ToListAsync();

        var amenitiesCount = await FindAll(trackChanges).CountAsync();

        return PagedList<Amenity>.ToPagedList(amenities, amenitiesCount,
            amenityParams.PageNumber, amenityParams.PageSize);
    }

    public void RemoveAmenity(Amenity amenity) => Delete(amenity);

    public async Task<Amenity?> GetAmenityByName(string name, bool trackChanges = false)
    {
        return await FindByCondition(am =>
            am.Name.Equals(name), trackChanges)
            .SingleOrDefaultAsync();
    }
}