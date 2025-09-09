namespace RoyalHall.DTOs.BookingDTOs
{
    public class GetBookingDTO
    {
        public int BookingId { get; set; }
        public int CustomerId { get; set; }
        public int HallId { get; set; }
        public string? HallName { get; set; }
        public string? PersonName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string BookingType { get; set; } = null!;
        public DateOnly BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool IsApproved { get; set; } = false;


    }
}
