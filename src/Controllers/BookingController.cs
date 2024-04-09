using System.Text.Json;
using BookARoom.Dto;
using BookARoom.Interfaces;
using BookARoom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookAGuest;

[ApiController]
[Route("api/v1/bookings")]
public sealed class BookingController : ControllerBase
{
    private readonly IServiceManager _service;

    public BookingController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}", Name = "GetBooking")]
    public async Task<IActionResult> GetSingleBooking(int id)
    {
        var booking = await _service.BookingService
            .GetSingleBookingAsync(id, trackChanges: false);

        return Ok(booking);
    }

    [HttpGet]
    public async Task<IActionResult> GetManyBookings([FromQuery] BookingParameters bookingParams)
    {
        var (bookings, pageMetaData) = await _service.BookingService
                .GetManyBookingsAsync(bookingParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, bookings);
    }


    /// <summary>
    /// Only authorized staff can update a booking
    /// </summary>
    /// <param name="id"></param>
    /// <param name="GuestForUpdate"></param>
    /// <returns></returns>
    // [HttpPut("{id:int}")]
    // [ServiceFilter(typeof(ValidateDtoFilter))]
    // public async Task<IActionResult> UpdateGuest(int id, [FromBody] BookingUpdateDto booking)
    // {
    //     await _service.BookingService.UpdateBookingAsync(id, booking);

    //     return NoContent();
    // }

    /// <summary>
    /// Only authorized staff can delete a booking
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveBooking(int id)
    {
        await _service.BookingService.RemoveBookingAsync(id);

        return NoContent();
    }
}
