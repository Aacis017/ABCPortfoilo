using ABCPortfolio.Data;
using ABCPortfolio.Models;
using ABCPortfolio.Services;
using ABCPortfolio.Services.IServices;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;
using NToastNotify;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllersWithViews();



builder.Services.ConfigureApplicationCookie(option =>
{
    option.LoginPath = "/admin/login";
});
builder.Services.AddDbContext<AppDBContext>(options => options.
UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<AppDBContext>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICareerServices, CareerServices>();
builder.Services.AddScoped<IserviceServices, ServiceServices>();
//session register
//builder.Services.AddSession();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = AuthConfigModel.CookieScheme;
    options.DefaultChallengeScheme = AuthConfigModel.CookieScheme;
    options.DefaultSignInScheme = AuthConfigModel.CookieScheme;
}).AddCookie("Cookies", option =>
{

}).
AddPolicyScheme(AuthConfigModel.CookieScheme, AuthConfigModel.CookieScheme, option =>
{
    option.ForwardDefaultSelector = context =>
    {
        return "Cookies";
    };
});

//Toaster



var app = builder.Build();                    

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//code for auto migration
//using (var scope = app.Services.CreateScope())
//{
//    var db = scope.ServiceProvider.GetRequiredService<AppDBContext>();
//    //db.Database.Migrate();
//}
//end
//app.UseSession();
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
 
app.UseAuthentication();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
 