using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace RoyalHall.Models;

public partial class Booking
{
    public int BookingId { get; set; }
    public int CustomerId { get; set; }
    public int HallId { get; set; }

   

    public string BookingType { get; set; } = null!;

    public DateOnly BookingDate { get; set; }

    public decimal TotalPrice { get; set; }

    public bool IsApproved { get; set; } = false;

    [ForeignKey(nameof(CustomerId))]
    public virtual Customer Customer { get; set; } = null!;
    [ForeignKey(nameof(HallId))]
    public virtual Hall Hall { get; set; } = null!;
}
