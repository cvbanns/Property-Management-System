﻿using HotelManagement.Core.DTOs;
using HotelManagement.Core.IServices;
using Microsoft.AspNetCore.Mvc;

namespace HotelManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {

        private readonly IHotelService _hotelService;
        

        public HotelController(IHotelService hotelService)
        {

            _hotelService = hotelService;
            
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var hotels = await _hotelService.GetHotels();
            return Ok(hotels);
        }
        [HttpGet("{Id}")]
        public async Task<IActionResult> GetById([FromRoute] string Id)
        {
            var hotel = await _hotelService.GetHotelById(Id);
            return Ok(hotel);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateById(UpdateHotelDto update, string Id)
        {
            var hotel = await _hotelService.UpdateHotel(update, Id);
            return Ok(hotel);
        }
        [HttpGet("Rooms-By-Availability")]
        public async Task<IActionResult> GetRoomsByAvailability(string HotelName,string RoomType)
        {
            var result = await _hotelService.GetRoomsByAvailability(HotelName,RoomType);
            if(!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("Ratings")]
        public async Task<IActionResult> GetHotelRatings(string HotelName)
        {
            var result = await _hotelService.GetHotelRating(HotelName);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
        [HttpGet("Available-Rooms-By-Id")]
        public async Task<IActionResult> AvailableRoomsById(string HotelName, string RoomId)
        {
            var result = await _hotelService.GetHotelRoomsById(HotelName,RoomId);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> Create(AddHotelDto hotelDto)
        {
            var result = await _hotelService.Create(hotelDto);
            if (!result.Succeeded) return BadRequest(result);
            return Ok(result);
        }
    }
}
