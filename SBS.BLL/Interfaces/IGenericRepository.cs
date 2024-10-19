using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface IGenericRepository<T> where T : ModelsBase
{
	public void Add(T entity);

	public void Delete(T entity);

	public  Task<T> GetById(int id);
	public  Task<IEnumerable<T>> GetAll();
	public void Update(T entity);
	public int Save();
}
