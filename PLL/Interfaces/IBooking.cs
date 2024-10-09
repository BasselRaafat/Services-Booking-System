using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.PLL.Interfaces
{
    public interface IBooking
    {
        public void Add(Booking obj);

        public void Update(Booking obj);

        public void Delete(int id);

        public List<Booking> GetAll();
        public Booking GetById(int id);

        public void Save();
    }
}
