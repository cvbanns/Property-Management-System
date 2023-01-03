﻿using HotelManagement.Core.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelManagement.Core.IRepositories
{
    public interface IRoomRepository: IGenericRepository<Room>
    {
        void Add(string Hotel_ID, Room room);

    }
}