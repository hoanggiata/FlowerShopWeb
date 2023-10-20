using AeFLOWER.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System.Security.Claims;

namespace AeFLOWER.Controllers
{
    public class LoginRegController : Controller
    {
        private readonly ILogger<LoginRegController> _logger;
        private readonly FlowershopContext db;

        public LoginRegController(ILogger<LoginRegController> logger, FlowershopContext db)
        {
            _logger = logger;
            this.db = db;
        }

        private string GetUserID()
        {
            var userID = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            return userID;
        }

        public IActionResult Index(string returnURL)
        {
            LoginVM loginModel = new LoginVM();
            loginModel.ReturnUrl = returnURL;
            return View(loginModel);
        }

        [HttpPost]
        public async Task<IActionResult> Index(LoginVM loginModel)
        {
            var hashpass = Utility.HashingPassword(loginModel.Password);
            if(ModelState.IsValid)
            {
                var user = db.Accounts.FirstOrDefault( x => x.Username == loginModel.Username&& x.Password == hashpass );
                if(user!= null)
                {
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name,user.Username),
                        new Claim(ClaimTypes.NameIdentifier,user.AccountId.ToString()),
                    };
                    var identity = new ClaimsIdentity(claims,CookieAuthenticationDefaults.AuthenticationScheme);
                    var principal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, new AuthenticationProperties()
                    {
                        IsPersistent = loginModel.RememberLogin
                    });
                    return Redirect(loginModel.ReturnUrl);
                }
                else
                {
                    ViewBag.Error = "<div class='error'>Sai tên tài khoản hoặc mật khẩu</div>";
                    return View(user);
                }
            }
            return View(loginModel);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return LocalRedirect("/");
        }

        public IActionResult Reg(string returnUrl)
        {
            RegVM reg = new RegVM();
            reg.returnUrl = returnUrl;
            return View(reg);
        }

        [HttpPost]
        public IActionResult Reg(RegVM reg)
        {
            if (ModelState.IsValid)
            {
                bool checkUserExsist = db.Accounts.Any(x => x.Username == reg.UserName);
                if (checkUserExsist)
                {
                    ViewBag.UserNameMessage = "<div class='error'>Đã có tài khoản này trong hệ thống, vui lòng tạo tài khoản khác</div>";
                    return View();
                }
                bool checkEmailExsist = db.Accounts.Any(x => x.Email == reg.Email);
                if (checkEmailExsist)
                {
                    ViewBag.EmailMessage = "<div class='error'>Email này đã được sử dụng, vui lòng nhập email khác </div>";
                    return View();
                }
                Account user = new Account();
                user.Username = reg.UserName;
                user.Email = reg.Email;
                user.Password = reg.PassWord;
                Utility utility = new Utility();
                utility.CreateUser(user);

                return RedirectToAction("Index",reg.returnUrl);
            }
            return View();
        }

        public IActionResult ForgotPassword()
        {
            TempData["cssDisplay"] = "none";
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            Account getAccount = db.Accounts.FirstOrDefault(x => x.Email == email);
            if(getAccount != null)
            {
                string newPass = getAccount.Password = Utility.CreateRandomPassword();
                getAccount.Password = Utility.HashingPassword(getAccount.Password);
                db.Accounts.Update(getAccount);
                db.SaveChanges();
                Utility.SendEmail(getAccount,newPass);
                ViewBag.EmailMessage = "<div class='success'>Password has been sent to your email address</div>";
                TempData["cssDisplay"] = "block";
                await Task.Delay(300);
                return View();
            }
            else
            {
                ViewBag.EmailMessage = "<div class='error'>This email address does not match our records</div>";
                TempData["cssDisplay"] = "none";
                return View();  
            }
        }
    }
}
