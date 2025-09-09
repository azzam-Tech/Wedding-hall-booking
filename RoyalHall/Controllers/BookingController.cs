using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoyalHall.Data;
using RoyalHall.DTOs.BookingDTOs;
using RoyalHall.DTOs.HallDTOs;
using RoyalHall.Models;
using RoyalHall.Services;

namespace RoyalHall.Controllers
{
    public class BookingController(BookingHallContext _context, CustomerServices customerServices, BookingServices bookingServices) : Controller
    {
        private readonly BookingHallContext _context;



        // GET: Booking
        public IActionResult AddBooking(int hallId, string hallName, decimal price)
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["Error"] = "Please log in to book a hall.";
                return RedirectToAction("Login", "Customer");
            }
            //var customer = HttpContext.Session.GetInt32("UserId");
            //var customerDetails = customerServices.GetCustomerByIdAsync(customer.Value).Result;

            ViewBag.HallId = hallId;
            ViewBag.HallName = hallName;
            ViewBag.Price = price;
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddBooking(AddBookingDTO booking)
        {
            if (ModelState.IsValid)
            {
                if (HttpContext.Session.GetInt32("UserId") == null)
                {
                    TempData["Error"] = "Please log in to book a hall.";
                    return RedirectToAction("Login", "Customer");
                }

                var customerId = (int)HttpContext.Session.GetInt32("UserId");
                booking.HallId = booking.HallId;
                string proccess = await bookingServices.AddBookingAsync(booking , customerId);
                return RedirectToAction("BookingDetails", "Booking");
            }
            return View("AddBooking", booking);
        }

        // GET: BookingDetails
        public async Task<IActionResult> BookingDetails()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                TempData["Error"] = "Please log in to book a hall.";
                return RedirectToAction("Login", "Customer");
            }

            var customerId = (int)HttpContext.Session.GetInt32("UserId");
            var isAdmin = (int)HttpContext.Session.GetInt32("IsAdmin");
            if(isAdmin == 0)
            {
                ViewBag.IsAdmin = isAdmin;
                var bookings = bookingServices.GetBookingsByCustomerIdAsync(customerId).Result; // استرجاع كافة الحجوزات من قاعدة البيانات
                return View(bookings);

               
            }
            else
            {
                ViewBag.IsAdmin = isAdmin;
                var bookings = bookingServices.GetAllBookingAsync().Result; // استرجاع كافة الحجوزات من قاعدة البيانات
                return View(bookings);
            }

        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var booking = await bookingServices.GetBookingByIdAsync(id);
            //var booking = await _context.Bookings.Include(b => b.Customer).Include(b => b.Hall).FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                return NotFound();
            }
            ViewBag.HallPrice = booking.TotalPrice;
            return View(booking);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(UpdateBookingDTO BookChange)
        {
            if (!ModelState.IsValid)
            {
              
               
                return RedirectToAction("BookingDetails");
            }

            string proccess = bookingServices.UpdateBookingAsync(BookChange.BookingId, BookChange).Result;


            // إعادة توجيه المستخدم إلى صفحة تفاصيل الحجز بعد الحفظ
            return RedirectToAction("BookingDetails", new { id = BookChange.BookingId });
        }

        // GET: Booking/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var booking = await bookingServices.GetBookingByIdAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking);
        }

        // POST: Booking/Delete/5
        // POST: Booking/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int bookingId)
        {

            var booking = await bookingServices.DeleteBookingAsync(bookingId);
            if (booking == null)
            {
                return NotFound();
            }
            return RedirectToAction("BookingDetails");
        }

        public async Task<IActionResult> Approve(int id)
        {
            var booking = await bookingServices.ApproveBookingAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return RedirectToAction("BookingDetails");
        }

        // GET: Booking/Confirm/5
        public async Task<IActionResult> Confirm(int id)
        {
            var booking = await _context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return NotFound();
            }

            return View(booking); // عرض صفحة التأكيد
        }

        // POST: Booking/Confirm/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ConfirmBooking(int id)
        {
            HttpContext.Session.SetInt32($"Confirmed_{id}", 1);
            TempData["Success"] = "Your booking has been confirmed.";
            return RedirectToAction("BookingDetails");
        }


    }
}
    