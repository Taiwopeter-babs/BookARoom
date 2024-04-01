using System.Text.Json;
using BookARoom.Dto;
using BookARoom.Interfaces;
using BookARoom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookAGuest;

[ApiController]
[Route("api/v1/guests/{guestId:int}/bookings")]
public sealed class BookingController : ControllerBase
{
    private readonly IServiceManager _service;

    public BookingController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}", Name = "GetBooking")]
    public async Task<IActionResult> GetBooking(int guestId, int id)
    {
        var booking = await _service.BookingService
            .GetBookingAsync(guestId, id, trackChanges: false);

        return Ok(booking);
    }

    [HttpGet]
    public async Task<IActionResult> GetBookings([FromQuery] BookingParameters bookingParams)
    {
        var (bookings, pageMetaData) = await _service.BookingService
                .GetBookingsAsync(bookingParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, bookings);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> AddBooking(int guestId, [FromBody] BookingCreationDto booking)
    {
        var addedBooking = await _service.BookingService.AddBookingAsync(guestId, booking);

        return CreatedAtRoute("GetBooking", new { guestId, addedBooking.Id }, addedBooking);
    }

    /// <summary>
    /// Only authorized staff can update a booking
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
    /// Only authorized staff can delete a booking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveBooking(int guestId, int id)
    {
        await _service.BookingService.RemoveBookingAsync(guestId, id);

        return NoContent();
    }
}
