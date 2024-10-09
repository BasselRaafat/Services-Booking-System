using Microsoft.EntityFrameworkCore;

namespace MVCDemo.Models
{
    public class AppContext : DbContext
    {
        public DbSet<Booking> Booking { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Service> Service { get; set; }
        public DbSet<Technician> Technician { get; set; }
        public DbSet<TechnicianService> TechnicianService { get; set; }
        public DbSet<ServiceCategory> ServiceCategory { get; set; }

        //In case overload 
        public AppContext() : base()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer
                ("Data Source=.;Initial Catalog=MVCDemo;Integrated Security=True;Encrypt=False;Trust Server Certificate=True");
            base.OnConfiguring(optionsBuilder);
        }

    }
}
