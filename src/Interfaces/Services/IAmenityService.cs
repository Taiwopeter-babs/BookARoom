using BookARoom.Dto;
using BookARoom.Utilities;

namespace BookARoom.Interfaces;

public interface IAmenityService
{
    Task<AmenityDto> AddAmenityAsync(AmenityCreationDto amenityCreationDto);

    Task<AmenityDto> GetAmenityAsync(int amenityId, bool includeRoom,
        bool trackChanges = false);

    Task<(IEnumerable<AmenityDto>, PageMetadata pageMetadata)> GetAmenitiesAsync(
        AmenityParameters amenityParameters, bool trackChanges = false);

    Task UpdateAmenityAsync(int amenityId, AmenityUpdateDto amenityUpdateDto,
        bool trackChanges = true);

    Task RemoveAmenityAsync(int amenityId, bool trackChanges = false);
}