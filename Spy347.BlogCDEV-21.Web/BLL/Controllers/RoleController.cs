using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Controllers
{
    [Route("[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private IMapper _mapper;
        private readonly ILogger<RoleController> _logger;

        public RoleController(IRoleService roleService, IMapper mapper, ILogger<RoleController> logger)
        {
            _roleService = roleService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// [Get] создание роли
        /// </summary>
        [Route("Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult AddRole()
        {
            return View();
        }

        /// <summary>
        /// [Post] создание роли
        /// </summary>
        [Route("Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> AddRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var roleId = await _roleService.CreateRole(model);
                _logger.LogInformation($"Созданна роль - {model.Name}");
                return RedirectToAction("Get", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] редактирования роли
        /// </summary>
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditRole(Guid id)
        {
            try
            {
                var model = await _roleService.EditRole(id);
    
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось изменить роль по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Role");
            }
        }

        /// <summary>
        /// [Post] редактирования роли
        /// </summary>
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _roleService.EditRole(model);
                _logger.LogDebug($"Измененна роль - {model.Name}");
                return RedirectToAction("Get", "Role");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] удаления роли
        /// </summary>
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveRole(Guid id, bool isConfirm = true)
        {
            try
            {
                if (isConfirm)
                    await RemoveRole(id);
                return RedirectToAction("Get", "Role");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить роль по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Role");
            }
        }

        /// <summary>
        /// [Post] удаления роли
        /// </summary>
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveRole(Guid id)
        {
            await _roleService.RemoveRole(id);
            _logger.LogDebug($"Удаленна роль - {id}");
            return RedirectToAction("Get", "Role");
        }

        /// <summary>
        /// [Get] Метод, получения всех тегов
        /// </summary>
        [Route("Get")]
        [HttpGet]
        [Authorize(Roles = "Администратор, Модератор")]
        public async Task<IActionResult> GetRoles()
        {
            var roles = await _roleService.GetRoles();
            return View(roles);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}