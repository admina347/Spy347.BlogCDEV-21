using System.Diagnostics;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.Models;
using Spy347.BlogCDEV_21.Web.ViewModels.Account;

namespace Spy347.BlogCDEV_21.Web.Controllers;

public class HomeController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly RoleManager<Role> _roleManager;
    private readonly IHomeService _homeService;
    private IMapper _mapper;
    private readonly ILogger<HomeController> _logger;

    public HomeController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IHomeService homeService, IMapper mapper, ILogger<HomeController> logger)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _roleManager = roleManager;
        _homeService = homeService;
        _mapper = mapper;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        //await _homeService.GenerateData();

        return View(new MainViewModel());
    }

    [Authorize]
    public IActionResult MyPage()
    {

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [Route("Home/Error")]
    public IActionResult Error(int? statusCode = null)
    {
        if (statusCode.HasValue)
        {
            if (statusCode == 404 || statusCode == 500)
            {
                var viewName = statusCode.ToString();
                _logger.LogWarning($"Произошла ошибка - {statusCode}\n{viewName}");
                return View(viewName);
            }
            else
                return View("500");
        }
        return View("500");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
