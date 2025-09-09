using Microsoft.EntityFrameworkCore;
using RoyalHall.Data;
using RoyalHall.DTOs.CustomerDTOs;
using RoyalHall.Models;

namespace RoyalHall.Services
{

    public class CustomerServices(BookingHallContext context)
    {
       public async Task<string> Register(AddCustomerDTO model)
       {
            if (string.IsNullOrEmpty(model.UserName) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
            {
                return "All fields are required"; 
            }
            if (await context.Customers.AnyAsync(c => c.UserName == model.UserName))
            {
                return "Username already exists"; // رسالة خطأ إذا كان اسم المستخدم موجودًا بالفعل
            }
            var customer = new Customer
            {
                UserName = model.UserName,
                Password = model.Password,
                Email = model.Email,
                IsAdmin = false,
                PhoneNumber = model.PhoneNumber,
                Address = model.Address


            };
            context.Customers.Add(customer);
            await context.SaveChangesAsync(); // حفظ التغييرات في قاعدة البيانات
            return "Registration successful"; // رسالة نجاح التسجيل
       }

        public async Task<Dictionary<string,Customer?>> Login(LoginCustomerDTO model)
        {
            var customer = await context.Customers
                .FirstOrDefaultAsync(c => c.UserName == model.UserName && c.Password == model.Password);
            if (customer == null)
            {
                Dictionary<string, Customer?> keyValuePairs = new Dictionary<string, Customer?>();
                keyValuePairs.Add("Invalid username or password", null);
                return keyValuePairs; 
            }
            Dictionary<string, Customer?> keyValuePairs2 = new Dictionary<string, Customer?>();
            keyValuePairs2.Add("Login successful", customer);
            return keyValuePairs2;
        }

        public async Task<List<GetCustomerDTO>> GetAllCustomers()
        {
            var customer = await context.Customers.ToListAsync();
            var customersDTO = customer.Select(c => new GetCustomerDTO
            {
                CustomerId = c.CustomerId,
                UserName = c.UserName,
                Email = c.Email,
                IsAdmin = c.IsAdmin,
                PhoneNumber = c.PhoneNumber,
                Address = c.Address
            }).ToList();
            return customersDTO;
        }

        public async Task<GetCustomerDTO> GetCustomerByIdAsync(int id)
        {
            var customer = await context.Customers.FindAsync(id);
            if (customer == null)
            {
                return null;
            }
            var customerDTO = new GetCustomerDTO
            {
                CustomerId = customer.CustomerId,
                UserName = customer.UserName,
                Email = customer.Email,
                Password = customer.Password,
                IsAdmin = customer.IsAdmin,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };
            return customerDTO;
        }


    }
}
