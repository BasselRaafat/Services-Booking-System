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

public class GenericRepository<T> : IGenericRepository<T> where T : ModelsBase
{
	private protected AppDbContext _dbContext;
	public GenericRepository(AppDbContext dbcontext)
	{
		_dbContext = dbcontext;
	}
	public void Add(T entity)
	{
		_dbContext.Add(entity);
	}

	public void Delete(T entity)
	{
		_dbContext.Set<T>().Remove(entity);
	}

	public async Task<T> GetById(int id)
	{
		return  await _dbContext.FindAsync<T>(id);

	}

	public async Task<IEnumerable<T>> GetAll()
	{

		return await _dbContext.Set<T>().AsNoTracking().ToListAsync();
	}

	public void Update(T entity)
	{
		_dbContext.Set<T>().Update(entity);
	}
    public async Task<int> CountAllAsync()
    {
        return await _dbContext.Set<T>().CountAsync();
    }
	public int Save()
	{
		return _dbContext.SaveChanges();
	}

}
