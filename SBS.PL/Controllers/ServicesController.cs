using BookingService.BLL.Interfaces;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services_Booking_System.Helpers;
using Services_Booking_System.View_Models;

namespace Services_Booking_System.Controllers;

public class ServicesController : Controller
{
    private readonly IServiceRepository _serviceRepo;
    public ServicesController(IServiceRepository serviceRepo) 
    {
        _serviceRepo = serviceRepo;
    }
    public IActionResult Index(int id)
    {
        var services =_serviceRepo.GetByCategoryId(id);
        ViewBag.CategoryId = id;
        return View(services);
    }

    [HttpGet]
    public IActionResult Create(int? id)
    {
        if(id is not null)
        {
            var service = new ServiceViewModel() 
            {
                CategoryId=id.Value,
            };
            return View(service);
        }
        return View();
    }

    [HttpPost]
    public IActionResult Create(ServiceViewModel record)
    {
        if (ModelState.IsValid)
        {
            var picName = Files.UploadFile(record.Photo, "Images");
            var service = new Service()
            {
                Name = record.Name,
                Description = record.Description,
                CategoryId = record.CategoryId,
                PhotoName = picName,
            };
            _serviceRepo.Add(service);
            _serviceRepo.Save();
            return RedirectToAction("Index", new {id= record.CategoryId });
        }
        return View(record);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var serv = await _serviceRepo.GetById(id);

        if (serv is not null)
        {
            var service = new ServiceViewModel()
            {
                Id = serv.Id,
                Name = serv.Name,
                Description = serv.Description,
                CategoryId = serv.CategoryId,
                PhotoName= serv.PhotoName,
            };
            return View(service);
        }
        return BadRequest("Not Found");
    }
    [HttpPost]
    public IActionResult Edit(ServiceViewModel service)
    {
        if (ModelState.IsValid) 
        {
            if (service.PhotoName is not null)
                Files.DeleteFile(service.PhotoName, "Images");
            var picName =Files.UploadFile(service.Photo, "Images");
            var mappedService = new Service() 
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                CategoryId = service.CategoryId,
                PhotoName = picName
            };
            _serviceRepo.Update(mappedService);
            _serviceRepo.Save();
            return RedirectToAction("Index", new { id = service.CategoryId });
        }
        return View(service);
    }

    public IActionResult Delete(int id) 
    {
        return View();
    }
}
