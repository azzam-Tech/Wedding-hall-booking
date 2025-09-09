namespace RoyalHall.DTOs.BookingDTOs
{
    public class UpdateBookingDTO
    {
        public int BookingId { get; set; }
        public string BookingType { get; set; } 
        public DateOnly BookingDate { get; set; }
        public decimal TotalPrice { get; set; }


    }
}
