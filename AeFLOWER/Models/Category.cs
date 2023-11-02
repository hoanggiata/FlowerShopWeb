using System;
using System.Collections.Generic;

namespace AeFLOWER.Models;

public partial class Category
{
    public string IdCategory { get; set; } = null!;

    public string? NameCategory { get; set; }

    public string? CssClass { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();


    public List<Category> GetCate()
    {
        FlowershopContext db = new FlowershopContext();

        List<Category> categories = db.Categories.ToList();
        return categories;
    }
}
