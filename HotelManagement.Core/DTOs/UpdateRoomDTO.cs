using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.DTOs
{
    public class UpdateRoomDTO
    {        
        public string RoomNo { get; set; }
        public bool IsBooked { get; set; }        
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
