﻿using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface IBookingRepository : IGenericRepository<Booking>
{
    public IEnumerable<Booking> GetWithUserId(int UserId);
    public IEnumerable<Booking> GetWithTechId(int  TechId);
}
