using BookARoom.Dto;
using BookARoom.Interfaces;
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
        var room = await _service.RoomService.GetRoomAsync(id);

        return Ok(room);
    }

    [HttpPost]
    public async Task<IActionResult> AddRoom([FromBody] RoomForCreationDto room)
    {
        var addedRoom = await _service.RoomService.AddRoomAsync(room);

        return CreatedAtRoute("GetRoom", new { addedRoom.Id }, addedRoom);
    }
}
