using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class ShoppingCart
{
    public int IdCart { get; set; }

    public string IdUser { get; set; } = null!;

    public DateTime? CartTime { get; set; }

    public string? PhoneNonAccount { get; set; }

    public string? NameCusNonAccount { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Account IdUserNavigation { get; set; } = null!;
}
