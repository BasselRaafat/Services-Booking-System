using BookingService.BLL.Interfaces;
using BookingService.BLL.Repositories;
using BookingService.DAL.Data;
using BookingService.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using Services_Booking_System.Helpers;
using Services_Booking_System.View_Models;
using System.Data;

namespace Services_Booking_System.Controllers;

public class CategoryController : Controller
{
	private readonly ICategoryRepository _categoryRepo;

    public CategoryController(ICategoryRepository categoryRepo)
	{
		_categoryRepo = categoryRepo;
    }

    public IActionResult Index()
	{
			return View();
	}


	//Admin
	[HttpGet]
	public IActionResult Create()
	{
		return View();
	}
    
	
	[HttpPost]
	public IActionResult Create(CategoryViewModel _category)
	{
		if (ModelState.IsValid)
		{
			var photoname = Files.UploadFile(_category.Photo, "images");
			var category = new Category()
			{
				Name = _category.Name,
				PhotoName = photoname
				
			};
            _categoryRepo.Add(category);
			_categoryRepo.Save();
			return RedirectToAction(nameof(Index));
		}
		return View(_category);
	}
	//Admin

	[HttpGet]
 ///public async Task<IActionResult> Edit(int id) 
///{
///var category = await _categoryRepo.GetById(id);
///var categoryViewModel = new CategoryViewModel()
///{
///	Name = category.Name,
///	PhotoName = category.PhotoName,
///};
///return View(categoryViewModel);
///}
///A//dmin
///[//HttpPost]
 ///public IActionResult Edit(CategoryViewModel _category)
 ///{
 ///    if (ModelState.IsValid)
 ///    {
///	if(_category.PhotoName is not null)
///		Files.DeleteFile(_category.PhotoName, "Images");
 ///        var photoname = Files.UploadFile(_category.Photo, "images");
 ///        var category = new Category()
 ///        {
 ///            Name = _category.Name,
 ///            PhotoName = photoname
 ///
 ///        };
 ///        _categoryRepo.Update(category);
 ///        _categoryRepo.Save();
 ///        return RedirectToAction(nameof(Index));
 ///    }
 ///    return View(_category);
 ///}
 ///Admin

    public IActionResult Delete(int id) 
	{
		return View();
	}
    //Admin
    public IActionResult Detail(int id) 
	{
		return View();
	}
}
