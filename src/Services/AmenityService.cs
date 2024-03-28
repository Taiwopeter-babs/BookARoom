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

    public async Task<AmenityDto> GetAmenityAsync(int amenityId, bool trackChanges = false)
    {
        var amenity = await CheckAmenity(amenityId, trackChanges);

        return _mapper.Map<AmenityDto>(amenity);
    }

    public async Task<(IEnumerable<AmenityDto>, PageMetadata pageMetadata)> GetAmenitiesAsync(
        AmenityParameters amenityParams, bool trackChanges = false)
    {
        var amenitiesWithPageData = await _repository.Amenity.GetAmenitiesAsync(amenityParams, trackChanges);

        var amenitiesDtos = _mapper.Map<IEnumerable<AmenityDto>>(amenitiesWithPageData);

        return (amenitiesDtos, pageMetadata: amenitiesWithPageData.PageMetadata);
    }

    public async Task RemoveAmenityAsync(int amenityId, bool trackChanges = false)
    {
        var amenity = await CheckAmenity(amenityId, trackChanges);

        _repository.Amenity.RemoveAmenity(amenity);

        await _repository.SaveAsync();
    }

    public async Task UpdateAmenityAsync(int amenityId, AmenityUpdateDto amenityUpdateDto,
        bool trackChanges = true)
    {
        var amenity = await CheckAmenity(amenityId, trackChanges);

        _mapper.Map(amenityUpdateDto, amenity);

        await _repository.SaveAsync();
    }

    private async Task<Amenity> CheckAmenity(int amenityId, bool trackChanges)
    {
        var amenity = await _repository.Amenity.GetAmenityAsync(amenityId, trackChanges) ??
            throw new AmenityNotFoundException(amenityId);

        return amenity;
    }
}