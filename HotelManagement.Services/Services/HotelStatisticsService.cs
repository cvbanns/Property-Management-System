using HotelManagement.Core;
using HotelManagement.Core.IRepositories;
using HotelManagement.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Services.Services
{
    public class HotelStatisticsService : IHotelStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public HotelStatisticsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        

        public async Task<Response<string>> GetTotalNumberOfHotels()
        {
            var hotels = await _unitOfWork.hotelRepository.GetAllAsync();
            if (hotels == null)
            {
                return new Response<string>
                {
                    StatusCode = 404,
                    Succeeded = false,
                    Data = null,
                    Message = "No Hotel found"

                };
            }
            var noOfHotels = hotels.Count();
            return new Response<string>
            {
				StatusCode = 202,
				Succeeded = true,
				Data = noOfHotels.ToString(),
				Message = "Successful"
				
            };
		}

		public async Task<Response<string>> GetTotalNumberOfRooms(string Id)
		{
			var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
			if (getHotel == null)
			{

				return new Response<string>
				{
					StatusCode = 404,
					Succeeded = false,
					Data = null,
					Message = "No Hotel found"

				};
			}
			var noOfRoomsInTheHotel = getHotel.RoomTypes.Count();
			return new Response<string>
			{
				StatusCode = 202,
				Succeeded = true,
				Data = noOfRoomsInTheHotel.ToString(),
				Message = "Successful"

			};
			 
		}

		public async Task<Response<string>> GetTotalNumberOfRoomsOccupied(string Id)
		{
			var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
			if (getHotel == null)
			{
				return new Response<string>
				{
					StatusCode = 404,
					Succeeded = false,
					Data = null,
					Message = "No Hotel found"

				};
			}
			var noOfRoomsInTheHotelOccupied = getHotel.RoomTypes.Where(x => x.Available != 0).Count();
			return new Response<string>
			{
				StatusCode = 202,
				Succeeded = true,
				Data = noOfRoomsInTheHotelOccupied.ToString(),
				Message = "Successful"

			};
			
		}

		public async Task<Response<string>> GetTotalNumberOfRoomsUnoccupied(string Id)
		{
			var getHotel = await _unitOfWork.hotelRepository.GetByIdAsync(x => x.Id == Id);
			if (getHotel == null)
			{
				return new Response<string>
				{
					StatusCode = 404,
					Succeeded = false,
					Data = null,
					Message = "No Hotel found"

				};
			}
			var noOfRoomsInTheHotelUnoccupied = getHotel.RoomTypes.Where(x => x.Available == 0).Count();
			return new Response<string>
			{
				StatusCode = 202,
				Succeeded = true,
				Data = noOfRoomsInTheHotelUnoccupied.ToString(),
				Message = "Successful"

			};
	

		}
	}
}
