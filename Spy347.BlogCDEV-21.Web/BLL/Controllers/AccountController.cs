using System.Net;
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
        [Route("Login")]
        [HttpGet]
        public IActionResult Login()
        {
            try
            {
                return View();
            }
            catch (Exception ex)
            {
                return StatusCode(401);
            }
        }

        /// <summary>
        /// [Post] login
        /// </summary>
        [Route("Login")]
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
        [Route("Register")]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// [Post] регистрация
        /// </summary>
        [Route("Register")]
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
        /// [Get] новы пользователь
        /// </summary>
        [Route("Add")]
        [HttpGet]
        public IActionResult AddAccount()
        {
            return View();
        }

        /// <summary>
        /// [Post] добавить нового пользователя
        /// </summary>
        [Route("Add")]
        [HttpPost]
        public async Task<IActionResult> AddAccount(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountService.AddUser(model);

                if (result.Succeeded)
                {
                    _logger.LogInformation($"Создан аккаунт - {model.Email}");
                    return RedirectToAction("Get", "Account");
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
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditAccount(Guid id)
        {
            try
            {
                var model = await _accountService.EditAccount(id);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось получить пользователя по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Account");
            }
        }

        /// <summary>
        /// [Post] редактирование
        /// </summary>
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditAccount(UserEditViewModel model)
        {
            var result = await _accountService.EditAccount(model);

            if (result.Succeeded)
            {
                _logger.LogDebug($"Аккаунт - {model.UserName} был изменен");
                return RedirectToAction("Get", "Account");
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
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveAccount(Guid id, bool confirm = true)
        {
            try
            {
                if (confirm)
                    await RemoveAccount(id);
                return RedirectToAction("Get", "Account");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить пользователя по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Account");
            }
        }

        /// <summary>
        /// [Post] удаление аккаунта
        /// </summary>
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveAccount(Guid id)
        {
            await _accountService.RemoveAccount(id);
            _logger.LogDebug($"Аккаунт {id} удалён");

            return RedirectToAction("Get", "Account");
        }

        /// <summary>
        /// [Post] выхода из аккаунта
        /// </summary>
        [Route("Logout")]
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
        [Route("Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var users = await _accountService.GetAccounts();

            return View(users);
        }
    }
}