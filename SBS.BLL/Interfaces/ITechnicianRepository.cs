﻿using BookingService.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.BLL.Interfaces;

public interface ITechnicianRepository
{
    public void Add(Technician obj);

    public void Update(Technician obj);

    public void Delete(int id);

    public List<Technician> GetAll();
    public Technician GetById(int id);

    public void Save();
}
