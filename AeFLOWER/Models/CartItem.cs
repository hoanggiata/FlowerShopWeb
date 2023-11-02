using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }

    public int IdShoppingCart { get; set; }

    public string IdProduct { get; set; } = null!;

    public int? PaymentId { get; set; }

    public int QuantityItem { get; set; }

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual ShoppingCart IdShoppingCartNavigation { get; set; } = null!;


    public static List<CartItem> GetAllCartItem()
    {
        FlowershopContext db = new FlowershopContext();
        List<CartItem> list_cartItem = db.CartItems.ToList();
        return list_cartItem;
    }

    public static List<CartItem> GetAllCartItemByIdSC(int id_ShoppingCart)
    {
        FlowershopContext db = new FlowershopContext();
        List<CartItem> list_cartItem = db.CartItems.Where(x=>x.IdShoppingCart == id_ShoppingCart).ToList();
        return list_cartItem;
    }
}
