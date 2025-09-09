using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using RoyalHall.DTOs.HallDTOs;
using RoyalHall.Models;
using RoyalHall.Services;

namespace RoyalHall.Controllers
{
    public class HomeController(HallServices hallServices) : Controller
    {
        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public IActionResult Index()
        {
            var isAdmin = HttpContext.Session.GetInt32("IsAdmin");
            var isLogin = HttpContext.Session.GetInt32("UserId");
            ViewBag.IsAdmin = isAdmin;
            ViewBag.IsLogin = isLogin;

            List<GetHallDTO> halls = hallServices.GetHallsAsync().Result;
            return View(halls);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SupportPage()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
