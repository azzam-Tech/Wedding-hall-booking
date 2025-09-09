using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using RoyalHall.Data;
using RoyalHall.DTOs.CustomerDTOs;
using RoyalHall.Models;
using RoyalHall.Services;

namespace RoyalHall.Controllers
{
    public class CustomerController(CustomerServices customerServices) : Controller
    {
        private BookingHallContext db = new BookingHallContext(); // اتصل بقاعدة البيانات

        // عرض النموذج (الفورم)
        [HttpGet]
        public ActionResult Registeration()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Registeration(AddCustomerDTO model)
        {

            if (ModelState.IsValid)
            {
                string registerProccess = await customerServices.Register(model);
                if (registerProccess == "Registration successful")
                return RedirectToAction("Login");
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid Registeration attempt.");
                }
            }

            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginCustomerDTO model)
        {
            if (ModelState.IsValid)
            {

                Dictionary<string, Customer?> loginProcess = await customerServices.Login(model);

                if (loginProcess.ContainsKey("Login successful"))
                {
                    Customer customer = loginProcess["Login successful"]!;
                    HttpContext.Session.SetInt32("UserId", customer.CustomerId);
                    HttpContext.Session.SetInt32("IsAdmin", customer.IsAdmin is true ? 1 : 0);
                    // Redirect to a dashboard or home page after successful login  
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Add an error message to the model state  
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }
            }

            return View(model);
        }


    }
}
