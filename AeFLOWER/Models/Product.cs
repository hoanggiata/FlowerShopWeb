using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class Product
{
    public string IdProduct { get; set; } = null!;

    public string? NameProduct { get; set; }

    public string? Newcash { get; set; }

    public string? Oldcash { get; set; }

    public string? Size { get; set; }

    public string? IdCate { get; set; }

    public string? Imageurl { get; set; }

    public string? Desproduct { get; set; }

    public string? MainFlower { get; set; }

    public string? SubFlower { get; set; }

    public int? StarSum { get; set; }

    public int? Quantity { get; set; }

    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? IdCateNavigation { get; set; }

    public List<Product> GetTopProduct()
    {
        FlowershopContext db = new FlowershopContext();
        var query = db.Products.ToList();
        List<Product> result = new List<Product>();
        foreach (Product item in query)
        {
            if (item.StarSum > 4) result.Add(item);
        }
        return result;
    }

    public List<Product> GetProductExById(string id)
    {
        FlowershopContext db = new FlowershopContext();
        var query = db.Products.ToList();
        List<Product> result = new List<Product>();
        foreach (Product item in query)
        {
            if (item.IdProduct != id)
                result.Add(item);
        }
        return result;
    }

    public List<Product> GetProductByCate(string idCate)
    {
        FlowershopContext db = new FlowershopContext();
        var list = db.Products.ToList();
        List<Product> result = new List<Product>();
        foreach(Product item in list)
        {
            if (item.IdCate == idCate)
                result.Add(item);
        }
        return result;
    }
}
