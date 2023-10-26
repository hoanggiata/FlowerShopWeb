using AeFLOWER.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.CodeAnalysis.FlowAnalysis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = new PathString("/LoginReg/Index");
    options.ReturnUrlParameter = "urlRedirect";
    options.ExpireTimeSpan = TimeSpan.FromDays(10);
    options.SlidingExpiration = true;
});
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(temp => { 
    temp.Cookie.Name = "flowershopsession";
    temp.IdleTimeout = new TimeSpan(0, 30, 0);
});
builder.Services.AddDbContext<FlowershopContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapDefaultControllerRoute();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
