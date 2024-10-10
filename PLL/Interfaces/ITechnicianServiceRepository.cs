using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface ITechnicianServiceRepository
{
    public void Add(TechnicianService obj);

    public void Update(TechnicianService obj);

    public void Delete(int id);

    public List<TechnicianService> GetAll();
    public TechnicianService GetById(int id);

    public void Save();
}
