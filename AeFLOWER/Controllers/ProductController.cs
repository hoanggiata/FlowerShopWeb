using AeFLOWER.Models;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Dynamic;
using System.Security.Claims;
using X.PagedList;

namespace AeFLOWER.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IWebHostEnvironment webHostEnvironment;
        private readonly FlowershopContext db;
        public const string CartKey = "cart";
     

        public ProductController(IWebHostEnvironment webHostEnvironment, ILogger<ProductController> logger, FlowershopContext db)
        {
            this.webHostEnvironment = webHostEnvironment;
            _logger = logger;
            this.db = db;
        }
        [HttpGet]
        public IActionResult Index(int page, string cate = "cat01")
        {
            dynamic myModel = new ExpandoObject();
            // myModel.Books = GetBookByCate(cate);
            Pagination pagination = new Pagination();
            Product books = new Product();
            Category cateModel = new Category();
            ViewBag.totalPage = pagination.getTotalPage(cate);
            ViewBag.OnePageOfProducts = books.GetProductByCate(cate).ToPagedList(page, 8);
            ViewBag.CurrentPage = page;
            ViewBag.RecommendBooks = books.GetProductByCate(cate);
            myModel.Cate = cateModel.GetCate();
            foreach (Category item in myModel.Cate)
            {
                if (item.IdCategory == cate)
                {
                    item.CssClass = "active";
                    ViewBag.GetCate = item.IdCategory;
                }
            }
            ViewBag.ListCate = myModel.Cate;
            return View();
        }

        //Lấy ảnh từ thư mục
        public IOrderedEnumerable<IFileInfo> getImages(string cate, string id)
        {
            var provider = new PhysicalFileProvider(webHostEnvironment.WebRootPath);
            var content = provider.GetDirectoryContents(Path.Combine("IMG","products", cate, id));
            var objFiles = content.OrderBy(m => m.LastModified);
            return objFiles;
        }

        [HttpGet]
        public IActionResult ProductDetail(string id, string cate)
        {
            FlowershopContext db = new FlowershopContext();
            Product product = db.Products.Find(id);
            //Get image Cover
            var objFiles = getImages(cate, id);
            List<string> Images = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                Images.Add(item.Name);
            }
            ViewBag.images = Images; //Done

            return View(product);
        }

        //Lay Cart tu Session (danh sach Cart Item)
        List<CartItem> GetCartItems()
        {
            var session = HttpContext.Session;
            string jsonCart = session.GetString(CartKey);
            if (jsonCart != null)
                return JsonConvert.DeserializeObject<List<CartItem>>(jsonCart);

            return new List<CartItem>();
        }
        //Xoa Cart khoi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CartKey);
        }

        //Luu Cart(Danh sach CartItem) vao session
        void SaveCartSession(List<CartItem>ls)
        {
            var session = HttpContext.Session;
            string jsonCart = JsonConvert.SerializeObject(ls);
            session.SetString(CartKey, jsonCart);
        }

        public IActionResult AddToCart(string productID)
        {
            var product = db.Products.Find(productID);
            if (product == null)
                return NotFound("Không có sản phẩm");

            var cart = GetCartItems();
            var cartItem = cart.Find(p => p.IdProduct == productID);
            if(cartItem!=null)
            {
                cartItem.QuantityItem++;
            }
            else
                cart.Add(new CartItem() { QuantityItem = 1, product = product });

            return RedirectToAction(nameof(Cart));

        }

        public IActionResult RemoveCart(string productID)
        {
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult UpdateCart(int quantity,string productID)
        {
            return RedirectToAction("Index");
        }

        public IActionResult Cart()
        {
            return View(GetCartItems());
        }

        public IActionResult CheckOut()
        {
            return View();
        }
    }
}
