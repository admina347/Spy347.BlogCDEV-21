using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Spy347.BlogCDEV_21.Infrastructure;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL;
using Spy347.BlogCDEV_21.Web.Extentions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
//UoW add Repositories
builder.Services.AddUnitOfWork()
.AddCustomRepository <Post, PostRepository>();

//Identity password settings
builder.Services.AddIdentity<User, IdentityRole>(opts => {
  opts.Password.RequiredLength = 5;   
  opts.Password.RequireNonAlphanumeric = false;  
  opts.Password.RequireLowercase = false; 
  opts.Password.RequireUppercase = false; 
  opts.Password.RequireDigit = false;
  })
  .AddEntityFrameworkStores<ApplicationDbContext>();

// Подключаем автомаппинг
var mapperConfig = new MapperConfiguration((v) => 
{
    v.AddProfile(new MappingProfile());
}
);

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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
