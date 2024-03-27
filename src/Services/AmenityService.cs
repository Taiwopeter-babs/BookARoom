using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Utilities;

namespace BookARoom.Services;

public class AmenityService : IAmenityService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;


    public AmenityService(IRepositoryManager repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<AmenityDto> AddAmenityAsync(AmenityCreationDto amenityCreationDto)
    {
        var amenity = await _repository.Amenity.GetAmenityByName(amenityCreationDto.Name);
        if (amenity != null)
            throw new AmenityAlreadyExistsException(amenityCreationDto.Name);

        var amenityEntity = _mapper.Map<Amenity>(amenityCreationDto);
        _repository.Amenity.AddAmenity(amenityEntity);

        await _repository.SaveAsync();

        return _mapper.Map<AmenityDto>(amenityEntity);
    }

    public Task<AmenityDto> GetAmenityAsync(int amenityId, bool includeRoom,
        bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task<(IEnumerable<AmenityDto>, PageMetadata pageMetadata)> GetAmenitiesAsync(
        AmenityParameters amenityParameters, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task RemoveAmenityAsync(int amenityId, bool trackChanges = false)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAmenityAsync(int amenityId, AmenityUpdateDto amenityUpdateDto, bool trackChanges = true)
    {
        throw new NotImplementedException();
    }
}