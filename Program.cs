using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using InterakcjeMiedzyLekami.Models;
using InterakcjeMiedzyLekami.Services;
using InterakcjeMiedzyLekami.Services.Interfaces;
using InterakcjeMiedzyLekami.Configuration.Mapper;
using System.Configuration;
using InterakcjeMiedzyLekami.Configuration.Settings;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
//builder.Services.AddAutoMapper(typeof(ActiveSubstancesMapper));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/User/LoginUser";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireLoggedIn", policy =>
    {
        policy.RequireAuthenticatedUser();
    });
    options.AddPolicy("AdminAccess", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireRole("admin");
    });
});


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.Configure<Settings>(builder.Configuration.GetSection("Settings"));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IInteractions, InteractionsService>();
builder.Services.AddScoped<IReviews, ReviewsService>();

builder.Services.AddAutoMapper(typeof(MainProfile));

var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
