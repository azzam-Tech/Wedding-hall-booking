using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHall.Data;
using RoyalHall.DTOs.HallDTOs;
using RoyalHall.Models;
using RoyalHall.Services;

namespace RoyalHall.Controllers
{
    public class HallController(HallServices hallServices, BookingHallContext context,IImageService imageService) : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ShowDetailsHall1(int id)
        {
            var halldetails = hallServices.GetHallByIdAsync(id).Result;
           

            return View("ShowDetailsHall1", halldetails);
        }



        [HttpGet]
        public IActionResult AddHall()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult> AddHall(AddHallDTO hall)
        {
            if (ModelState.IsValid)
            {
                if (hall.ImageUrl != null && hall.ImageUrl.Length > 0)
                {
                    string uploadFolderPath = "uploads/halls"; 
                    string imageUrl = await imageService.SaveImageAsync(hall.ImageUrl, uploadFolderPath);

                    if (!string.IsNullOrEmpty(imageUrl))
                    {
                        var newHall = new Hall
                        {
                            Name = hall.Name,
                            Description = hall.Description,
                            ImageUrl = imageUrl,
                            Location = hall.Location,
                            Price = hall.Price,
                            Capacity = hall.Capacity
                        };

                        context.Halls.Add(newHall);
                        await context.SaveChangesAsync();

                        return RedirectToAction("Index","Home"); 
                    }
                    else
                    {
                        ModelState.AddModelError("ImageUrl", "فشل في تحميل الصورة.");
                        return View(hall);
                    }
                }
                else
                {
                    ModelState.AddModelError("ImageUrl", "يرجى تحديد صورة للقاعة.");
                    return View(hall);
                }
            }
            return View(hall);
        }
    }

       
 }


