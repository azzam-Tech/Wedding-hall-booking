namespace RoyalHall.DTOs.HallDTOs
{
    public class AddHallDTO
    {
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public IFormFile ImageUrl { get; set; } = null!;
        public string Location { get; set; } = null!;
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}
