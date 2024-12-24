using BLL.Context;
using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(config =>
    {
        config.LoginPath = "/Users/Login";
        config.AccessDeniedPath = "/Users/Login";
        config.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        config.SlidingExpiration = true;
    });

// AppSettings:
var appSettingsSection = builder.Configuration.GetSection(nameof(AppSettings));
appSettingsSection.Bind(new AppSettings());

// IoC Container:
var connectionString = builder.Configuration.GetConnectionString("Db");
builder.Services.AddDbContext<Db>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddScoped<IService<director,DirectorModel>, DirectorService>();
builder.Services.AddScoped<IService<role,RoleModel>, RoleService>();
builder.Services.AddScoped<IService<movie,MovieModel>, MovieService>();
builder.Services.AddScoped<IService<genre,GenreModel>, GenreService>();
builder.Services.AddScoped<IService<user,UserModel>, UserService>();

builder.Services.AddHttpContextAccessor();
builder.Services.AddSingleton<HttpServiceBase, HttpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

// Authentication:
app.UseAuthentication();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();