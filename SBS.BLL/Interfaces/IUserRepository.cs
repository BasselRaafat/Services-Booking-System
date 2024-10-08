﻿using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface IUserRepository
{
    public void Add(User obj);

    public void Update(User obj);

    public void Delete(int id);

    public List<User> GetAll();
    public User GetById(int id);

    public void Save();
}
