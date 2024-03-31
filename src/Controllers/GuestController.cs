using System.Text.Json;
using BookARoom.Dto;
using BookARoom.Interfaces;
using BookARoom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookAGuest;

[ApiController]
[Route("api/v1/guests")]
public sealed class GuestController : ControllerBase
{
    private readonly IServiceManager _service;

    public GuestController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}", Name = "GetGuest")]
    public async Task<IActionResult> GetGuest(int id)
    {
        var Guest = await _service.GuestService.GetGuestAsync(id, trackChanges: false);

        return Ok(Guest);
    }

    [HttpGet]
    public async Task<IActionResult> GetGuests([FromQuery] GuestParameters guestParams)
    {
        var (guests, pageMetaData) = await _service.GuestService
                .GetGuestsAsync(guestParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, guests);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> AddGuest([FromBody] GuestCreationDto guest)
    {
        var addedGuest = await _service.GuestService.AddGuestAsync(guest);

        return CreatedAtRoute("GetGuest", new { addedGuest.Id }, addedGuest);
    }

    /// <summary>
    /// Only authorized staff can update a guest
    /// </summary>
    /// <param name="id"></param>
    /// <param name="GuestForUpdate"></param>
    /// <returns></returns>
    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> UpdateGuest(int id, [FromBody] GuestUpdateDto guestForUpdate)
    {
        await _service.GuestService.UpdateGuestAsync(id, guestForUpdate);

        return NoContent();
    }

    /// <summary>
    /// Only authorized staff can delete a guest
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveGuest(int id)
    {
        await _service.GuestService.RemoveGuestAsync(id);

        return NoContent();
    }
}
