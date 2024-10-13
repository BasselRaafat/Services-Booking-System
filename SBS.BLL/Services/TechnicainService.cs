using BookingService.BLL.Interfaces;
using BookingService.BLL.Repositories;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Services
{
    public class TechnicianServices
    {
        public TechnicianRepository TechnicianRepository { get; }
        public TechnicianServices(TechnicianRepository technicianRepository)
        {
            TechnicianRepository = technicianRepository;
        }

        public async Task<int> CreateTechnician
            (string firstname, string lastname,string email,string phonenumber,string city
            ,string address,string nationalId,string jobtitle, string imageurl,string bio)
        {
            Technician technician = new Technician()
            { FirstName = firstname, LastName = lastname, Email = email, PhoneNumber = phonenumber,
                City = city, Address = address, NationalID = nationalId ,
                JobTitle = jobtitle, ImageUrl = imageurl,Bio = bio,Rating = 0 };
            TechnicianRepository.Add(technician);
            TechnicianRepository.Save();
            return technician.Id;
        }
    }
}
