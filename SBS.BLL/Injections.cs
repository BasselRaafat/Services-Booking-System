using BookingService.BLL.Repositories;
using BookingService.BLL.Services;
using BookingService.DAL.Data;
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
            service.AddScoped<UserService>();
            service.AddScoped<TechnicianServices>();
            service.AddScoped<TechnicianRepository>();
        }
    }
}
