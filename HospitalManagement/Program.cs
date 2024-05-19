using HospitalManagement.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<_DbContext>(options => options.UseSqlServer(ConnectionString.GetConnectionString()));//added

builder.Services.AddAuthentication(options =>
{
options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;

}).AddCookie("Cookies", options =>
{
options.Cookie.HttpOnly = true;
options.Cookie.Name = "Cookies";
options.LoginPath = "/";
options.AccessDeniedPath = "/AccessDenied/Index";    
options.ExpireTimeSpan = TimeSpan.FromMinutes(20);
options.ReturnUrlParameter = "/";
options.SlidingExpiration = true;
});


//To prevent circular references error: For linq include (Best method)
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;

});

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

app.UseRouting();
app.UseAuthentication();//first
app.UseAuthorization();//second

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Index}/{id?}");

app.Run();
