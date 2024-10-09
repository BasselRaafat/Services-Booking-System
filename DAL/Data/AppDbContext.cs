using BookingService.DAL.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookingService.DAL.Data.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.SqlServer;
using System.Reflection;

namespace BookingService.DAL.Data;
public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<Booking> Booking { get; set; }
    public DbSet<User> User { get; set; }
    public DbSet<Category> Category { get; set; }
    public DbSet<Service> Service { get; set; }
    public DbSet<Technician> Technician { get; set; }
    public DbSet<TechnicianService> TechnicianService { get; set; }

    //In case overload 
    public AppDbContext(DbContextOptions options) : base(options)
    {
    }

	//protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	//{
	//    optionsBuilder.UseSqlServer
	//        ("Data Source=.;Initial Catalog=MVCDemo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
	//    base.OnConfiguring(optionsBuilder);
	//}
	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}

}