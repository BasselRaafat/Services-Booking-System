using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Repositories;

public class TechnicianServiceRepository
{
	private protected AppDbContext _dbContext;
	public TechnicianServiceRepository(AppDbContext dbcontext)
	{
		_dbContext = dbcontext;
	}
	public void Add(TechnicianService entity)
	{
		_dbContext.Add(entity);
	}

	public void delete(TechnicianService entity)
	{
		_dbContext.Set<TechnicianService>().Remove(entity);
	}

	public async Task<TechnicianService> Get(int ServiceId,int TechnicianId)
	{
		return await _dbContext.FindAsync<TechnicianService>(ServiceId,TechnicianId);

	}

	public async Task<IEnumerable<TechnicianService>> GetAll()
	{

		return await _dbContext.Set<TechnicianService>().AsNoTracking().ToListAsync();
	}

	public void Update(TechnicianService entity)
	{
		_dbContext.Set<TechnicianService>().Update(entity);
	}
	public int Save()
	{
		return _dbContext.SaveChanges();
	}
}
