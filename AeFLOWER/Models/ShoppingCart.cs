using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class ShoppingCart
{
    public int IdCart { get; set; }

    public string? IdUser { get; set; }

    public DateTime? CartTime { get; set; }

    public decimal? TotalPrice { get; set; }

    public string? PhoneNonAccount { get; set; }

    public string? NameCusNonAccount { get; set; }

    public string? OrderAddress { get; set; }

    public string? OrderNameNonAccount { get; set; }

    public string? OrderCity { get; set; }

    public string? OrderPhone { get; set; }

    public byte? OrderShipped { get; set; }

    public string? OrderEmail { get; set; }

    public string? OrderTrackingNumber { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Account? IdUserNavigation { get; set; }


    public static ShoppingCart CreateShoppingCart(string userID)
    {
        ShoppingCart shoppingCart = new ShoppingCart(); 
        shoppingCart.IdUser = userID;
        shoppingCart.OrderShipped = 0;
        return shoppingCart;
    }
}
