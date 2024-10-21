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
	public async Task<IActionResult> Edit(int id)
	{
		var category = await _categoryRepo.GetById(id);
		var categoryViewModel = new EditCategoryViewModel()
		{
			CategoryId = category.Id,
			Name = category.Name,
			PhotoName = category.PhotoName,
		};
		return View(categoryViewModel);
	}
 //Admin
 	[HttpPost]
	public IActionResult Edit(EditCategoryViewModel _category)
	{
		if (ModelState.IsValid)
		{
			string photoname;
			if (_category.PhotoName is not null && _category.Photo is not null)
			{
				Files.DeleteFile(_category.PhotoName, "Images");
			}
			if( _category.Photo is not null)
				photoname = Files.UploadFile(_category.Photo, "images");
			else
				photoname=_category.PhotoName;
			var category = new Category()
			{
				Id=_category.CategoryId,
				Name = _category.Name,
				PhotoName = photoname

			};
			_categoryRepo.Update(category);
			_categoryRepo.Save();
			return RedirectToAction(nameof(Index));
		}
		return View(_category);
	}
	//Admin

	//[HttpGet]
	//public IActionResult Delete(int id) 
	//{
	//	return View();
	//}

	[HttpPost]
    public async Task<IActionResult> Delete(int id)
	{
		Category cat= await _categoryRepo.GetById(id);
        if (cat.PhotoName is not null)
            Files.DeleteFile(cat.PhotoName, "Images");
        if (cat == null) {  return View("Error"); }
		_categoryRepo.Delete(cat);
		_categoryRepo.Save();
        return RedirectToAction("Index");
	}
    //Admin
    public IActionResult Detail(int id) 
	{
		return View();
	}
}
