using HotelManagement.Core.Domains;
using HotelManagement.Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IServices
{
    public interface IRoomService
    {
        Task<Response<GetRoomDto>> GetRoombyId(string Id);
        Task<Response<string>> AddRoom(string RoomType_ID, string Hotel_Name, AddRoomDto addRoomDto);
        Task<Response<GetRoomDto>> GetSingleRoom(string Id);
        Task<Response<string>> AddRoomType(string Hotel_Id, RoomTypeDTO roomType);
        Task<Response<string>> UpdateRoom(string Room_Id, UpdateRoomDTO updateRoom);
        Task<Response<string>> DeleteRoomById(string id);
    }
}
