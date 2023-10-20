using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class CartItem
{
    public int IdCartItem { get; set; }

    public int IdShoppingCart { get; set; }

    public string IdProduct { get; set; } = null!;

    public int QuantityItem { get; set; }

    public string Price { get; set; } = null!;

    public string CommentPro { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;

    public virtual ShoppingCart IdShoppingCartNavigation { get; set; } = null!;
}
