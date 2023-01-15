using HotelManagement.Core.Domains;
using HotelManagement.Core.IRepositories;
using HotelManagement.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Infrastructure.Repositories
{
    public class RoomTypeRepository : GenericRepository<RoomType>, IRoomTypeRepository
    {
        public RoomTypeRepository(HotelDbContext hotelDbContext) : base(hotelDbContext)
        {

        }
        public async void AddRoomType(string Hotel_Id, RoomType roomType)
        {
            roomType.HotelId = Hotel_Id;
            await AddAsync(roomType);
        }
    }
}
