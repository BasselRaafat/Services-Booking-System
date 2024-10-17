using BookingService.BLL.Interfaces;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

public class ServiceRepository : GenericRepository<Service>, IServiceRepository

{
	public IServiceRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}

    public IQueryable<Service> GetByCategoryId(int categoryId)
    {
        return _dbContext.Service.Where(s => s.CategoryId == categoryId).Include(s=>s.Category);
    }
}
