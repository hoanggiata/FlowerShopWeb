using AeFLOWER.Models;

namespace BookStoreWeb.Models
{
    public class Pagination
    {
        public Pagination() { }
        public double getTotalPage(string cate)
        {
            FlowershopContext db = new FlowershopContext();
            var allProduct = db.Products.ToList();
            List<Product> products = new List<Product>();
            foreach (Product item in allProduct)
            {
                if (item.IdCate == cate)
                {
                    products.Add(item);
                }
            }
            double totalPages = Math.Ceiling((double)products.Count() / 8);
            return (totalPages);
        }
    }
}
