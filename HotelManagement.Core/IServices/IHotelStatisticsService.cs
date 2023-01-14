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
       
        Task<Response<string>> GetTotalNumberOfRooms(string Id);
       
        Task<Response<string>> GetTotalNumberOfRoomsOccupied(string Id);
        
        Task<Response<string>> GetTotalNumberOfRoomsUnoccupied(string Id);
    }
}
