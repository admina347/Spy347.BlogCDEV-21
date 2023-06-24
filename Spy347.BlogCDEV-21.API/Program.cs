using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Spy347.BlogCDEV_21.Infrastructure;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL;
using Spy347.BlogCDEV_21.Web.BLL.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//БД
builder.Services.AddDbContext<ApplicationDbContext>(options =>
  options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/* builder.Services.AddSwaggerGen(c =>
            {
                var filepath = Path.Combine(AppContext.BaseDirectory, "Spuy347.BlogCDEV-21.API.xml");
                c.IncludeXmlComments(filepath);
            }); */


//Identity password settings
builder.Services.AddIdentity<User, Role>(opts =>
{
    opts.Password.RequiredLength = 5;
    opts.Password.RequireNonAlphanumeric = false;
    opts.Password.RequireLowercase = false;
    opts.Password.RequireUppercase = false;
    opts.Password.RequireDigit = false;
})
  .AddEntityFrameworkStores<ApplicationDbContext>();

// регистрация сервиса репозитория для взаимодействия с базой данных  
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

builder.Services.AddAuthentication(optionts => optionts.DefaultScheme = "Cookies")
  .AddCookie("Cookies", options =>
  {
    options.Events = new Microsoft.AspNetCore.Authentication.Cookies.CookieAuthenticationEvents
    {
      OnRedirectToLogin = redirectContext =>
      {
        redirectContext.HttpContext.Response.StatusCode = 401;
        return Task.CompletedTask;
      }
    };
  });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();