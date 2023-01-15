using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetRoomById(String Id)
        {
            var result = await _roomService.GetRoombyId(Id);
            if(!result.Succeeded) return BadRequest(result.Message);
            return Ok(result);
        }
        [HttpPost("add-room")]
        public async Task<IActionResult> AddRoom(string RoomType_ID, string Hotel_Name,[FromBody] AddRoomDto addRoomDto)
        {
            if (addRoomDto == null) return BadRequest("Room Not Created");
           var response=await _roomService.AddRoom(RoomType_ID, Hotel_Name, addRoomDto);
            return Ok(response);
        }

        [HttpGet("get-single-room")]
        public async Task<IActionResult> GetSingleRoom(string Id)
        {
            var room = await _roomService.GetSingleRoom(Id);
            return room.Succeeded ? Ok(room) : BadRequest(room.Message);
        }

        [HttpPost("add-roomtype")]
        public async Task<IActionResult> AddRoomType(string Hotel_Id, [FromBody] RoomTypeDTO roomType)
        {
            if(roomType == null) return BadRequest("Room Type not Created");
            var result = await _roomService.AddRoomType(Hotel_Id, roomType);
            return Ok(result);          
           
        }

        [HttpPut("update-room")]
        public async Task<IActionResult> UpdateRoom(string Room_Id, [FromBody] UpdateRoomDTO updateRoom)
        {

            if (updateRoom == null) return BadRequest("Room Type not Created");
            var result = await _roomService.UpdateRoom(Room_Id, updateRoom);
            return Ok(result);

        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            var result = await _roomService.DeleteRoomById(Id);
            if(!result.Succeeded) return BadRequest(result.Message);
            return Ok(result);
        }
    }
}
