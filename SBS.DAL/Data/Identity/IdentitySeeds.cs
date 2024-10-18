using BookingService.DAL.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;


namespace WEBPage.Models.Identity
{
    public static class IdentitySeeds
    {
        public static async Task SeedIdentityAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            await context.Database.MigrateAsync();  

            var usermanager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var rolemanager = scope.ServiceProvider.GetRequiredService<RoleManager<ApplicationRole>>();
            string[] RoleNames = { "Admin", "User", "Technician" };
            foreach(var role in RoleNames)
            {
                if (await rolemanager.RoleExistsAsync(role))
                    continue;
                else
                    await rolemanager.CreateAsync(new ApplicationRole { Name = role });
            }

            var AdminUser = new ApplicationUser { UserName = "admin@orderely.com", Email = "admin@orderely.com" };
            if (await usermanager.FindByEmailAsync(AdminUser.Email) == null)
            {
                AdminUser.Address = "Ismailia";
                AdminUser.FirstName = "Abdulrahman";
                AdminUser.LastName = "kamal";
                await usermanager.CreateAsync(AdminUser, "P@55w0rd");
                await usermanager.AddToRoleAsync(AdminUser, "Admin");
            }

        }
    }
}
