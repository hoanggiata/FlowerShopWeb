using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class Account
{
    public string AccountId { get; set; } = null!;

    public string? Username { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Address { get; set; }

    public string? Country { get; set; }

    public string? AccountDes { get; set; }

    public virtual ICollection<ShoppingCart> ShoppingCarts { get; set; } = new List<ShoppingCart>();
}
