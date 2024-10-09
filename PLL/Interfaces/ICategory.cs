using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.PLL.Interfaces
{
    public interface ICategory
    {
        public void Add(Category obj);

        public void Update(Category obj);

        public void Delete(int id);

        public List<Category> GetAll();
        public Category GetById(int id);

        public void Save();
    }
}
