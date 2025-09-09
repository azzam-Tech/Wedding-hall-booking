namespace RoyalHall.DTOs.BookingDTOs
{
    public class AddBookingDTO
    {
        public int HallId { get; set; }
        public string BookingType { get; set; } = null!;
        public DateOnly BookingDate { get; set; }
        public decimal TotalPrice { get; set; }

    }
}
