using BookingService.DAL.Data;
using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

internal class UserRepository : GenericRepository<User>
{
	public UserRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}
}
