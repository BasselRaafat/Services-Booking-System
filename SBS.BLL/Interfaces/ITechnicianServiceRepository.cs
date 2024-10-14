using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface ITechnicianServiceRepository
{
    public void Add(TechnicianService entity);

    public void Update(TechnicianService entity);

    public void Delete(TechnicianService entity);

    public Task<IEnumerable<TechnicianService>> GetAll();
    public Task<TechnicianService> GetById(int ServiceId, int TechnicianId);

    public int Save();
}
