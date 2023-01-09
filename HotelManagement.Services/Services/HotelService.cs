﻿using AutoMapper;
using HotelManagement.Core;
using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace HotelManagement.Services.Services
{
    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public HotelService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Response<GetHotelsDto>> GetHotelById(string Id)
        {
            var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedHotel = _mapper.Map<GetHotelsDto>(getHotel);
            if (mappedHotel == null)
            {
                return new Response<GetHotelsDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not fund"
                };
            }
            return new Response<GetHotelsDto>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = mappedHotel,
                Message = "Successful"
            };
        }

        public async Task<Response<List<GetHotelsDto>>> GetHotels()
        {
            var hotels = await _unitOfWork.hotelRepository.GetAllAsync();
            var allHotels = _mapper.Map<List<GetHotelsDto>>(hotels);
            if (allHotels.Count == 0)
            {
                return new Response<List<GetHotelsDto>>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotels not found"
                };
            }
            return new Response<List<GetHotelsDto>>
            {
                StatusCode = 202,
                Succeeded = true,
                Data = allHotels,
                Message = "Successful"
            };
        }

        public async Task<Response<UpdateHotelDto>> UpdateHotel(UpdateHotelDto update, string Id)
        {
            var updateHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
            var mappedUpdate = _mapper.Map(update, updateHotel);

            if (updateHotel == null)
            {
                return new Response<UpdateHotelDto>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "Hotel not found"
                };
            }
            _unitOfWork.SaveChanges();
            return Response<UpdateHotelDto>.Success("Updated Successfully", update);
        }
        public async Task<Response<List<GetHotelByRatingsDto>>> GetHotelRating(string HotelName)
        {
            try
            {
                var hotelRatings = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelName).Result.Ratings;
                var mappedHotelRating = _mapper.Map<List<GetHotelByRatingsDto>>(hotelRatings);

                if (mappedHotelRating == null) return Response<List<GetHotelByRatingsDto>>.Fail($"Hotel with {HotelName} Not Found");
                return Response<List<GetHotelByRatingsDto>>.Success(HotelName, mappedHotelRating);
            }
            catch (Exception ex)
            {
                
                return Response<List<GetHotelByRatingsDto>>.Fail("Error Loading...");
            }
        }

        public async Task<Response<List<GetRoomDto>>> GetRoomsByAvailability(string HotelNmae, string RoomType)
        {
            try
            {
                var roomsByAvailability = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name == HotelNmae)
                .Result.RoomTypes.Where(x => x.Name == RoomType).SelectMany(x => x.Rooms);
                var rooms = roomsByAvailability.Where(x => x.IsBooked == false).Select(x => x);
                var data = _mapper.Map<List<GetRoomDto>>(rooms);
                if (data == null) return Response<List<GetRoomDto>>.Fail($"{HotelNmae} Has No Room Available For {RoomType} RoomType");
                return Response<List<GetRoomDto>>.Success(HotelNmae, data);
            }
            catch (Exception ex)
            {
                
                return Response<List<GetRoomDto>>.Fail("Error Loading...");
            }
           
        }
        public async Task<Response<List<GetRoomDto>>> GetHotelRoomsById(string HotelName,string RoomId)
        {
            try
            {
                var rooms = _unitOfWork.hotelRepository.GetByIdAsync(x => x.Name.ToLower().Trim() == HotelName.ToLower().Trim())
                .Result.RoomTypes.SelectMany(x => x.Rooms).Where(x=>x.IsBooked == false && x.Id == RoomId);
                
                var data = _mapper.Map<List<GetRoomDto>>(rooms);
                if (data == null) return Response<List<GetRoomDto>>.Fail($" Room with Id {RoomId} is Not Available in Hotel {HotelName}");
                return Response<List<GetRoomDto>>.Success(HotelName, data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return Response<List<GetRoomDto>>.Fail("Error Loading...");
            }

        }
        public async Task<Response<Hotel>> Create(AddHotelDto hotelDto)
        {
            var mappedHotel = _mapper.Map<Hotel>(hotelDto);
            if (mappedHotel == null) return Response<Hotel>.Fail("Hotel not Added");
            await _unitOfWork.hotelRepository.AddAsync(mappedHotel);
            _unitOfWork.SaveChanges();
            return Response<Hotel>.Success("Successfully Created", mappedHotel);
        }
    }
}

       