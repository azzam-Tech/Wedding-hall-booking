using RoyalHall.Models;

namespace RoyalHall.DTOs.HallDTOs
{
    public class GetHallDTO
    {
        public int HallId { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string ImageUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Booking>? Bookings { get; }

    }
}
