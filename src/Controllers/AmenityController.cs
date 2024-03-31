using System.Text.Json;
using BookARoom.Dto;
using BookARoom.Interfaces;
using BookARoom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookAAmenity;

[ApiController]
[Route("api/v1/amenities")]
public sealed class AmenityController : ControllerBase
{
    private readonly IServiceManager _service;

    public AmenityController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}", Name = "GetAmenity")]
    public async Task<IActionResult> GetAmenity(int id)
    {
        var amenity = await _service.AmenityService.GetAmenityAsync(id);

        return Ok(amenity);
    }

    [HttpGet]
    public async Task<IActionResult> GetAmenities([FromQuery] AmenityParameters amenityParams)
    {
        var (amenities, pageMetaData) = await _service.AmenityService
                .GetAmenitiesAsync(amenityParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, amenities);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> AddAmenity([FromBody] AmenityCreationDto amenity)
    {
        var addedAmenity = await _service.AmenityService.AddAmenityAsync(amenity);

        return CreatedAtRoute("GetAmenity", new { addedAmenity.Id }, addedAmenity);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> UpdateAmenity(int id, [FromBody] AmenityUpdateDto amenityForUpdate)
    {
        await _service.AmenityService.UpdateAmenityAsync(id, amenityForUpdate);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveAmenity(int id)
    {
        await _service.AmenityService.RemoveAmenityAsync(id);

        return NoContent();
    }
}
