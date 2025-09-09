using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalHall.Data;
using RoyalHall.DTOs.BookingDTOs;
using RoyalHall.Models;
using System.Collections.Generic;

namespace RoyalHall.Services
{
    public class BookingServices(BookingHallContext context)
    {
        public async Task<List<GetBookingDTO>> GetAllBookingAsync()
        {
            var bookings = await context.Bookings.Include(b => b.Customer).Include(b => b.Hall).ToListAsync();
            var bookingsDto = bookings.Select(booking => new GetBookingDTO
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HallId = booking.HallId,
                HallName = booking.Hall.Name,
                PersonName = booking.Customer.UserName,
                Email = booking.Customer.Email,
                PhoneNumber = booking.Customer.PhoneNumber,
                Address = booking.Hall.Location,
                BookingType = booking.BookingType,
                BookingDate = booking.BookingDate,
                TotalPrice = booking.TotalPrice,
                IsApproved = booking.IsApproved
            }).ToList();
            return bookingsDto;
        }

        public async Task<string> AddBookingAsync(AddBookingDTO model,int customerId)
        {

            var booking = new Booking
            {
                CustomerId = customerId,
                HallId = model.HallId,
               
                BookingType = model.BookingType,
                BookingDate = model.BookingDate,
               
                TotalPrice = model.TotalPrice,
                IsApproved = false
            };
            context.Bookings.Add(booking);
            await context.SaveChangesAsync();
            return "Booking successful";
        }

        public async Task<string> UpdateBookingAsync(int id, UpdateBookingDTO model)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return "Booking not found";
            }


            booking.BookingType = model.BookingType;
            booking.BookingDate = model.BookingDate;
            booking.TotalPrice = model.TotalPrice;

            await context.SaveChangesAsync();
            return "Booking updated successfully";
        }
        
        public async Task<string> ApproveBookingAsync(int id)
        {
            var booking = await context.Bookings.FindAsync(id);
            if (booking == null)
            {
                return "Booking not found";
            }

            if(booking.IsApproved)
            {
                booking.IsApproved = false;
            }
            else
            {
                booking.IsApproved = true;
            }
            

            await context.SaveChangesAsync();
            return "Booking Approved successfully";
        }

        public async Task<string> DeleteBookingAsync(int id)
        {
            var booking = await context.Bookings.Include(b => b.Customer).Include(b => b.Hall).FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                return "book not found";
            }
            context.Bookings.Remove(booking);
            await context.SaveChangesAsync();
            return "Booking deleted successfully";
        }

        public async Task<GetBookingDTO> GetBookingByIdAsync(int id)
        {
            var booking = await context.Bookings.Include(b => b.Customer).Include(b => b.Hall).FirstOrDefaultAsync(b => b.BookingId == id);
            if (booking == null)
            {
                return null;
            }
            var bookingDto = new GetBookingDTO
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HallId = booking.HallId,
                HallName = booking.Hall.Name,
                PersonName = booking.Customer.UserName,
                Email = booking.Customer.Email,
                PhoneNumber = booking.Customer.PhoneNumber,
                Address = booking.Hall.Location,
                BookingType = booking.BookingType,
                BookingDate = booking.BookingDate,
                TotalPrice = booking.TotalPrice,
                IsApproved = booking.IsApproved
            };
            return bookingDto;
        }

        public async Task<List<GetBookingDTO>> GetBookingsByCustomerIdAsync(int customerId)
        {
            var bookings = await context.Bookings.Include(b => b.Customer).Include(b => b.Hall)
                .Where(b => b.CustomerId == customerId)
                .ToListAsync();
            var bookingsDto = bookings.Select(booking => new GetBookingDTO
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HallId = booking.HallId,
                HallName = booking.Hall.Name,
                PersonName = booking.Customer.UserName,
                Email = booking.Customer.Email,
                PhoneNumber = booking.Customer.PhoneNumber,
                Address = booking.Hall.Location,
                BookingType = booking.BookingType,
                BookingDate = booking.BookingDate,
                TotalPrice = booking.TotalPrice,
                IsApproved = booking.IsApproved
            }).ToList();
            return bookingsDto;
        }

        public async Task<List<GetBookingDTO>> GetBookingsByHallIdAsync(int hallId)
        {
            var bookings = await context.Bookings
                .Where(b => b.HallId == hallId)
                .ToListAsync();
            var bookingsDto = bookings.Select(booking => new GetBookingDTO
            {
                BookingId = booking.BookingId,
                CustomerId = booking.CustomerId,
                HallId = booking.HallId,
               
                BookingType = booking.BookingType,
                BookingDate = booking.BookingDate,
               
                TotalPrice = booking.TotalPrice,
                IsApproved = booking.IsApproved
            }).ToList();
            return bookingsDto;
        }

        public async Task<string> ApproveBooking(int bookingId)
        {
            var booking = await context.Bookings.FindAsync(bookingId);
            if (booking == null)
            {
                return "Booking not found";
            }
            booking.IsApproved = true;
            return "Booking Approved";
        }


    }
}
