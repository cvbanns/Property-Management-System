using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IHotelStatisticsService
    {
        Task<Response<string>> GetTotalNumberOfHotels();
        // Task<int> GetTotalNumberOfRooms(Hotel hotel);
        Task<Response<string>> GetTotalNumberOfRooms(string Id);
        // Task<int> GetTotalNumberOfRoomsOccupied(Hotel hotel);
        Task<Response<string>> GetTotalNumberOfRoomsOccupied(string Id);
        // Task<int> GetTotalNumberOfRoomsUnoccupied(Hotel hotel);
        Task<Response<string>> GetTotalNumberOfRoomsUnoccupied(string Id);
    }
}
