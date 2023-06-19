using AutoMapper;
using NLog.Web;
using Microsoft.EntityFrameworkCore;
using Spy347.BlogCDEV_21.Infrastructure;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL;
using Spy347.BlogCDEV_21.Web.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//Db
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));


//Identity password settings
builder.Services.AddIdentity<User, Role>(opts => {
  opts.Password.RequiredLength = 5;   
  opts.Password.RequireNonAlphanumeric = false;  
  opts.Password.RequireLowercase = false; 
  opts.Password.RequireUppercase = false; 
  opts.Password.RequireDigit = false;
  })
  .AddEntityFrameworkStores<ApplicationDbContext>();

//Register Services
builder.Services
  .AddTransient<ICommentRepository, CommentRepository>()
  .AddTransient<ITagRepository, TagRepository>()
  .AddTransient<IPostRepository, PostRepository>()
  .AddTransient<IAccountService, AccountService>()
  .AddTransient<ICommentService, CommentService>()
  .AddTransient<IHomeService, HomeService>()
  .AddTransient<IPostService, PostService>()
  .AddTransient<ITagService, TagService>()
  .AddTransient<IRoleService, RoleService>();

// Подключаем автомаппинг
var mapperConfig = new MapperConfiguration((v) => 
{
    v.AddProfile(new MappingProfile());
}
);
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);

// Connect logger
builder.Logging
  .ClearProviders()
  .SetMinimumLevel(Microsoft.Extensions.Logging.LogLevel.Trace)
  .AddConsole()
  .AddNLog("NLog");

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
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
