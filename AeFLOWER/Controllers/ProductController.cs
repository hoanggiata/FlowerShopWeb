using AeFLOWER.Models;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using Newtonsoft.Json;
using System.Dynamic;
using System.Net;
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

        private string GetUserID()
        {
            var userID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userID;
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


        //Get cart items from login user
        List<CartItem> GetCartItems()
        {
            List<CartItem> result = new List<CartItem>();
            List<CartItem> cart = new List<CartItem>();
            List<ShoppingCart> list_SC = db.ShoppingCarts.Where(x => x.IdUser == GetUserID()).ToList();
            list_SC.RemoveAll(x => x.OrderShipped == 1);

            var query = from c_item in CartItem.GetAllCartItem() //Left Data Source
                        join s_cart in list_SC //Right Data Source
                        on c_item.IdShoppingCart equals s_cart.IdCart //Inner Join Condition
                        into CartItemFromUserGroup //Performing LINQ Group Join
                        from shopping_cart in CartItemFromUserGroup.DefaultIfEmpty() //Performing Left Outer Join
                        select new { c_item, shopping_cart };
    
            foreach(var item in query)
            {
                result.Add(item.c_item);
            }

            return result;
        }

        //Get cart items from Non-Login User
        List<CartItem> GetCartItemsNonLogin()
        {
            var session = HttpContext.Session;
            string jsonCart = session.GetString(CartKey);
            if (jsonCart != null)
                return JsonConvert.DeserializeObject<List<CartItem>> (jsonCart);

            return new List<CartItem>();
        }

        //Xoa cart khoi session
        void ClearCart()
        {
            var session = HttpContext.Session;
            session.Remove(CartKey);
        }

        //Luu Cart vao Session
        void SaveCartSession(List<CartItem> list)
        {
            var session = HttpContext.Session;
            string jsonCart = JsonConvert.SerializeObject(list);
            session.SetString(CartKey, jsonCart);
        }

        //Non-login user them item vao cart
        public IActionResult AddToCartNonLogin(string productID, int quantity)
        {
            var checkProductExs = db.Products.Where(x => x.IdProduct == productID).FirstOrDefault();
            if (checkProductExs == null)
                return NoContent();

            var cart = GetCartItemsNonLogin();
            var cartItem = cart.Find(x => x.IdProduct == productID);
            if(cartItem !=null)
            {
                cartItem.QuantityItem++;
            }
            else
            {
                var temp = cart.LastOrDefault();

                if(temp !=null)                 
                cart.Add(new CartItem() { QuantityItem = quantity, IdProduct = productID, IdCartItem = temp.IdCartItem + 1 });

                else cart.Add(new CartItem() { QuantityItem = quantity, IdProduct = productID, IdCartItem = 1 });
            }
            SaveCartSession(cart);
            return NoContent();
        }


        //Login user Them item vao cart 
        public IActionResult AddToCart(string productID,int quantity)
        {
            var product = db.Products.Find(productID);
            ShoppingCart shoppingCart = db.ShoppingCarts.Where(x => x.IdUser == GetUserID() && x.OrderShipped == 0).FirstOrDefault();
            if(shoppingCart == null)
            {
                shoppingCart = ShoppingCart.CreateShoppingCart(GetUserID());
                db.ShoppingCarts.Add(shoppingCart);
                db.SaveChanges();
            }
            
            bool checkNullListCart = db.CartItems.Where(x=>x.IdShoppingCart == shoppingCart.IdCart).Any();
            if (!checkNullListCart)
            {
                CartItem item = new CartItem();
                item.IdShoppingCart = shoppingCart.IdCart;
                item.IdProduct = productID;
                item.QuantityItem = quantity;
                db.CartItems.Add(item);
                db.SaveChanges();
                return NoContent();
            }

            List<CartItem> list_CartItem = CartItem.GetAllCartItemByIdSC(shoppingCart.IdCart);
            foreach (CartItem item in list_CartItem)
            {
                if (item.IdProduct == productID)
                {
                    item.QuantityItem++;
                    db.CartItems.Update(item);
                    db.SaveChanges();
                    return NoContent();
                }
            }

            CartItem addItem = new CartItem();
           // addItem.IdCartItem = list_CartItem.OrderBy(x => x.IdCartItem).Last().IdCartItem + 1;
            addItem.IdProduct = productID;
            addItem.QuantityItem = quantity;
            addItem.IdShoppingCart = shoppingCart.IdCart;
            db.CartItems.Add(addItem);
            db.SaveChanges();
            return NoContent();
           

            /*  */
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
        //Cart cho Login User
        public IActionResult Cart(int flag)
        {
            if (flag == 1)
            {
                ShoppingCart shoppingCart = db.ShoppingCarts.Where(x => x.IdUser == GetUserID() && x.OrderShipped == 0).FirstOrDefault();
                List<Product> list_product = Product.GetAllProduct();

                var query = from c_item in CartItem.GetAllCartItemByIdSC(shoppingCart.IdCart) //Left Data Source
                            join product in list_product //Right Data Source
                            on c_item.IdProduct equals product.IdProduct //Inner Join Condition
                            into CartItemFromUserGroup //Performing LINQ Group Join
                            from product in CartItemFromUserGroup.DefaultIfEmpty() //Performing Left Outer Join
                            select new { c_item, product };

                ViewBag.Query = query;
                ViewBag.Flag = flag;
                return View();
            }
            else
            {
                List<Product> list_product = Product.GetAllProduct();
                var query = from c_item in GetCartItemsNonLogin() //Left Data Source
                            join product in list_product //Right Data Source
                            on c_item.IdProduct equals product.IdProduct //Inner Join Condition
                            into CartItemFromUserGroup //Performing LINQ Group Join
                            from product in CartItemFromUserGroup.DefaultIfEmpty() //Performing Left Outer Join
                            select new { c_item, product };

                ViewBag.Query = query;
                ViewBag.Flag = flag;
                return View();
            }
        }

        public IActionResult CheckOut()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RemoveItem(string productID,int Flag)
        {
            if(Flag == 1)
            {
                string result = CartItem.RemoveItem(productID, GetUserID());
                return Json(result);
            }

            var cart = GetCartItemsNonLogin();
            var cartItem = cart.Find(x => x.IdProduct == productID);
            if (cartItem != null)
            {
                cart.Remove(cartItem);
                SaveCartSession(cart);
                return Json("Thanh Cong");
            }
            return Json("That bai");
        }
    }
}
