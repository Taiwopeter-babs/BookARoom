using AutoMapper;
using BookARoom.Dto;
using BookARoom.Exceptions;
using BookARoom.Interfaces;
using BookARoom.Models;
using BookARoom.Redis;
using BookARoom.Utilities;

namespace BookARoom.Services;

public class AmenityService : IAmenityService
{
    private readonly IRepositoryManager _repository;
    private readonly IMapper _mapper;
    private readonly IRedisService _redisService;

    public AmenityService(IRepositoryManager repository, IMapper mapper, IRedisService redisService)
    {
        _repository = repository;
        _mapper = mapper;
        _redisService = redisService;
    }


    public async Task<AmenityDto> AddAmenityAsync(AmenityCreationDto amenityCreationDto)
    {
        var amenity = await _repository.Amenity.GetAmenityByName(amenityCreationDto.Name!);
        if (amenity != null)
            throw new AmenityAlreadyExistsException(amenityCreationDto.Name!);

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

        // remove from cache
        string stringId = amenityId.ToString();
        await _redisService.DeleteAsync<Amenity>(stringId);
    }

    public async Task UpdateAmenityAsync(int amenityId, AmenityUpdateDto amenityUpdateDto,
        bool trackChanges = true)
    {
        var amenity = await CheckAmenity(amenityId, trackChanges);

        _mapper.Map(amenityUpdateDto, amenity);

        _repository.Amenity.UpdateModifiedTime(amenity);

        await _repository.SaveAsync();

        // Update in cache
        string stringId = amenityId.ToString();
        await _redisService.SaveObjectAsync(stringId, amenity);
    }

    private async Task<Amenity> CheckAmenity(int amenityId, bool trackChanges)
    {
        Amenity? amenity;
        string stringId = amenityId.ToString();

        amenity = await _redisService.GetValueAsync<Amenity>(stringId);

        // Cache miss: Get object from database and save in cache
        if (amenity == null || string.IsNullOrEmpty(amenity?.ToString()))
        {
            amenity = await _repository.Amenity.GetAmenityAsync(amenityId, trackChanges) ??
                throw new AmenityNotFoundException(amenityId);

            // save in redis cache

            await _redisService.SaveObjectAsync(stringId, amenity);
        }

        return amenity;
    }
}