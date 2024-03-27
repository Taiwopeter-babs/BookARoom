using System.Text.Json;
using BookARoom.Dto;
using BookARoom.Interfaces;
using BookARoom.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BookARoom;

[ApiController]
[Route("api/v1/rooms")]
public sealed class RoomController : ControllerBase
{
    private readonly IServiceManager _service;

    public RoomController(IServiceManager service)
    {
        _service = service;
    }

    [HttpGet("{id:int}", Name = "GetRoom")]
    public async Task<IActionResult> GetRoom(int id)
    {
        var room = await _service.RoomService.GetRoomAsync(id, includeAmenity: true);

        return Ok(room);
    }

    [HttpGet]
    public async Task<IActionResult> GetRooms([FromQuery] RoomParameters roomParams)
    {
        var (rooms, pageMetaData) = await _service.RoomService
                .GetRoomsAsync(roomParams, trackChanges: false);

        Response.Headers.Append("X-Pagination", JsonSerializer.Serialize(pageMetaData));

        return StatusCode(200, rooms);
    }

    [HttpPost]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> AddRoom([FromBody] RoomForCreationDto room)
    {
        var addedRoom = await _service.RoomService.AddRoomAsync(room);

        return CreatedAtRoute("GetRoom", new { addedRoom.Id }, addedRoom);
    }

    [HttpPut("{id:int}")]
    [ServiceFilter(typeof(ValidateDtoFilter))]
    public async Task<IActionResult> UpdateRoom(int id, [FromBody] RoomForUpdateDto roomForUpdate)
    {
        await _service.RoomService.UpdateRoomAsync(id, roomForUpdate);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> RemoveRoom(int id)
    {
        await _service.RoomService.RemoveRoomAsync(id);

        return NoContent();
    }
}
