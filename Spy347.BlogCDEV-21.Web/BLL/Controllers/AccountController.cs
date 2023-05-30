using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels.Account;

namespace Spy347.BlogCDEV_21.Web.BLL.Controllers
{
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<Role> roleManager, IAccountService accountService, IMapper mapper, ILogger<AccountController> logger)
        {
            _roleManager = roleManager;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _accountService = accountService;
            _logger = logger;
        }

        /// <summary>
        /// [Get] login
        /// </summary>
        [Route("Account/Login")]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// [Post] login
        /// </summary>
        [Route("Account/Login")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Login(model);

                if (result.Succeeded)
                    return RedirectToAction("Index", "Home");
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] регистрация
        /// </summary>
        [Route("Account/Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// [Post] регистрация
        /// </summary>
        [Route("Account/Register")]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.Register(model);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Создан аккаунт - {model.Email}");
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            return View(model);
        }

        /// <summary>
        /// [Get] редактирование
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(int id)
        {
            var model = await _accountService.EditAccount(id);
            return View(model);
        }

        /// <summary>
        /// [Post] редактирование
        /// </summary>
        [Route("Account/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserEditViewModel model)
        {
            var result = await _accountService.EditAccount(model);

            if (result.Succeeded)
            {
                _logger.LogDebug($"Аккаунт - {model.UserName} был изменен");
                return RedirectToAction("Index", "Home");
            }    
            else
            {
                ModelState.AddModelError("", $"{result.Errors.First().Description}");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(int id, bool confirm = true)
        {
            if (confirm)
                await RemoveAccount(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] удаление аккаунта
        /// </summary>
        [Route("Account/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveAccount(int id)
        {
            await _accountService.RemoveAccount(id);
            _logger.LogDebug($"Remove account {id}");

            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] выхода из аккаунта
        /// </summary>
        [Route("Account/Logout")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> LogoutAccount(int id)
        {
            await _accountService.LogoutAccount();
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] получения всех пользователей
        /// </summary>
        [Route("Account/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}