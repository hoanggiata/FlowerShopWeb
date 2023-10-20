using AeFLOWER.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AeFLOWER.Controllers
{
    public class ProfileController : Controller
    {
        private readonly ILogger<ProfileController> _logger;
        private readonly FlowershopContext db;

        public ProfileController(ILogger<ProfileController> logger, FlowershopContext db)
        {
            _logger = logger;
            this.db = db;
        }

        private string GetUserID()
        {
            var userId = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userId;
        }

        public IActionResult Index()
        {
            Account user = db.Accounts.Find(GetUserID());
            ViewBag.countryList = Utility.CountryList();
            return View(user);
        }

        [HttpPost]
        public IActionResult Index(string? username, string email, string? first_name, string? last_name, string? address, string? country, string? account_des)
        {
            Account user = db.Accounts.Find(GetUserID());
            user.Email = email;
            user.FirstName = first_name; user.LastName = last_name; user.Address = address; user.Country = country; user.AccountDes = account_des;
            db.Accounts.Update(user);
            db.SaveChanges();
            return Redirect("~/Profile/Index");
        }
    }
}
