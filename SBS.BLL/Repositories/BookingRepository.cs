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

public class BookingRepository: GenericRepository<Booking>,IBookingRepository
{
	public BookingRepository(AppDbContext dbcontext) : base(dbcontext)
	{
	}

	public IEnumerable<Booking> GetWithUserId(int UserId)
	{
		return _dbContext.Set<Booking>().Where(b=>b.UserID==UserId).Include(b=>b.User).Include(b=>b.Technician).ToList();
	}
	public IEnumerable<Booking> GetWithTechId(int TechId)
	{
		return _dbContext.Set<Booking>().Where(b=>b.TechnicianID==TechId).Include(b=>b.User).Include(b=>b.Technician).ToList();
	}

}
