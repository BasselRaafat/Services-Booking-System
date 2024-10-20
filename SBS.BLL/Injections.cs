﻿using BookingService.BLL.Interfaces;
using BookingService.BLL.Repositories;
using BookingService.BLL.Services;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingService.DAL
{
    public static class Injections
    {
        public static void AddInfrastructure(this IServiceCollection services ,IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                                                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")
                                               ));

        }
        public static void AddApplication(this IServiceCollection service,IConfiguration configuration)
        {
            service.AddScoped<UserRepository>();
            service.AddScoped<TechnicianRepository>();
            service.AddScoped<CategoryRepository>();
            service.AddScoped<ServiceRepository>();
            service.AddScoped<BookingRepository>();
            service.AddScoped<UserService>();
            service.AddScoped<TechnicianServices>();
            service.AddScoped<TechnicianRepository>();
            service.AddScoped<ICategoryRepository, CategoryRepository>();
            service.AddScoped<IServiceRepository, ServiceRepository>();
            service.AddScoped<IBookingRepository, BookingRepository>();
            service.AddScoped<IUserRepository,UserRepository>();
            service.AddScoped<ITechnicianRepository, TechnicianRepository>();

        }
    }
}
