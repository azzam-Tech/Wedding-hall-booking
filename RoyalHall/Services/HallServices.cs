using Microsoft.EntityFrameworkCore;
using RoyalHall.Data;
using RoyalHall.DTOs.HallDTOs;

namespace RoyalHall.Services
{
    public class HallServices(BookingHallContext context)
    {
        public async Task<List<GetHallDTO>> GetHallsAsync()
        {
            var halls = await context.Halls.ToListAsync();
            var hallDTOs = halls.Select(h => new GetHallDTO
            {
                HallId = h.HallId,
                Name = h.Name,
                Capacity = h.Capacity,
                Price = h.Price,
                Description = h.Description,
                ImageUrl = h.ImageUrl,
                Location = h.Location
            }).ToList();
            return hallDTOs;
        }

        public async Task<GetHallDTO> GetHallByIdAsync(int id)
        {
            var hall = await context.Halls.FindAsync(id);
            if (hall == null)
            {
                return null;
            }
            var hallDTO = new GetHallDTO
            {
                HallId = hall.HallId,
                Name = hall.Name,
                Capacity = hall.Capacity,
                Price = hall.Price,
                Description = hall.Description,
                ImageUrl = hall.ImageUrl,
                Location = hall.Location
            };
            return hallDTO;
        }

        
    }
}
