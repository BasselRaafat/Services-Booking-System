using BookingService.DAL.Data;
using Microsoft.EntityFrameworkCore;
using System;
using BookingService.DAL;
using WEBPage.Models.Identity;
using Microsoft.AspNetCore.Identity;
using BookingService.BLL.Services;
using BookingService.BLL.Repositories;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication(builder.Configuration);

builder.Services.AddIdentity<ApplicationUser, ApplicationRole>(option =>
{
    option.Password.RequiredLength = 8;
    option.Password.RequireDigit = true;
    option.Password.RequireNonAlphanumeric = false;
    option.Password.RequireUppercase = false;
}).AddEntityFrameworkStores<AppDbContext>().AddDefaultTokenProviders();



var app = builder.Build();

	await app.Services.SeedIdentityAsync();



app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


