using BLL.DAL;
using BLL.Models;
using BLL.Services;
using BLL.Services.Bases;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// builder.Services.AddDbContext<Db>(optionsAction =>
//     optionsAction.UseNpgsql("User ID=postgres;Password=patateskral;Host=localhost;Port=5432;Database=MovieManDB;"));

builder.Services.AddDbContext<Db>(options =>
    options.UseNpgsql("Host=localhost;Port=5432;Database=MovieManDB;Username=postgres;Password=patateskral;"));


builder.Services.AddScoped<IService<director,DirectorModel>, DirectorService>();

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

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();