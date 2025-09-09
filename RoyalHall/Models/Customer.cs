using System;
using System.Collections.Generic;

namespace RoyalHall.Models;

public partial class Customer
{
    public int CustomerId { get; set; }

    public string UserName { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsAdmin { get; set; } = false;
    public virtual ICollection<Booking>? Bookings { get; } 
}
