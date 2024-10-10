using BookingService.DAL.Data;
using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

public class ReviewRepository : GenericRepository<Review>
{
	public ReviewRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}
}
