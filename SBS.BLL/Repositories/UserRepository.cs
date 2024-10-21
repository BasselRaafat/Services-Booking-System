using BookingService.BLL.Interfaces;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

public class UserRepository : GenericRepository<User>, IUserRepository
{
	public UserRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}

	public User GetByEmail(string email)
	{
		return _dbContext.User.Where(u => u.Email == email)?.FirstOrDefault();
	}
}
