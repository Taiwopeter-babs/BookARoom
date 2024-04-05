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


    // [HttpGet("id/bookings", Name = "GetGuestBookings")]
    // public async Task<IActionResult> GetGuestBookings(int id)
    // {
    //     var guestAndBookings = await _service.BookingService
    //         .GetGuestBookingsAsync(id, trackChanges: false);

    //     return Ok(guestAndBookings);
    // }

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



    [HttpPost("{guestId:int}/bookings")]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> AddGuestBooking(int guestId, [FromBody] BookingCreationDto booking)
    {
        var addedBooking = await _service.BookingService.AddGuestBookingAsync(guestId, booking);

        return CreatedAtRoute("GetGuestSingleBooking", new { guestId, addedBooking.Id }, addedBooking);
    }


    [HttpGet("{guestId:int}/bookings/{id:int}", Name = "GetGuestSingleBooking")]
    public async Task<IActionResult> GetSingleGuestBooking(int guestId, int id)
    {
        var booking = await _service.BookingService
            .GetGuestSingleBookingAsync(guestId, id, trackChanges: false);

        return Ok(booking);
    }


    [HttpGet("{guestId:int}/bookings", Name = "GetGuestManyBookings")]
    public async Task<IActionResult> GetManyGuestBookings(int guestId,
        [FromQuery] BookingParameters bookingParams)
    {
        var (bookings, pageMetaData) = await _service.BookingService
                .GetGuestManyBookingsAsync(guestId, bookingParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, bookings);
    }
}
