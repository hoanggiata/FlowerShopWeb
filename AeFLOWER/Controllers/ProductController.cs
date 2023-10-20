using AeFLOWER.Models;
using BookStoreWeb.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System.Dynamic;
using X.PagedList;

namespace AeFLOWER.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        public ProductController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
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
    }
}
