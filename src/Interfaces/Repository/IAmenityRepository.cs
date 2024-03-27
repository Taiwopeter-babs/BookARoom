using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IAmenityRepository
{
    void AddAmenity(Amenity amenity);
    Task<Amenity?> GetAmenityAsync(int roomId, bool trackChanges = false);
    Task<PagedList<Amenity>> GetAmenitiesAsync(AmenityParameters amenityParameters,
        bool trackChanges = false);
    void RemoveAmenity(Amenity amenity);

    Task<Amenity?> GetAmenityByName(string name, bool trackChanges = false);
}