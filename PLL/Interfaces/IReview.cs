using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces
{
    public interface IReview
    {
        public void Add(Review obj);

        public void Update(Review obj);

        public void Delete(int id);

        public List<Review> GetAll();
        public Review GetById(int id);

        public void Save();
    }
}
