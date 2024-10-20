﻿using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface IServiceRepository : IGenericRepository<Service>
{
    public IQueryable<Service> GetByCategoryId(int categoryId);
}
