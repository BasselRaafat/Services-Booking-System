using BookingService.BLL.Interfaces;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

public class IServiceRepository : GenericRepository<Service>

{
	public IServiceRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}
}
