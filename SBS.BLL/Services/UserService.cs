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
    public class UserService
    {
        private readonly UserRepository userRepository;

        public UserService(UserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<int> CreateUser
            (string firstname, string lastname,string email,string phonenumber,string city,string address)
        {
            User user = new User()
            { FirstName = firstname, LastName = lastname, Email = email, PhoneNumber = phonenumber,
                City = city, Address = address };
            userRepository.Add(user);
            userRepository.Save();
            return user.Id;
        }
    }
}
