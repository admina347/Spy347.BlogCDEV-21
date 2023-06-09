using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Controllers
{
    [CustomAuthorize]
    [Route("[controller]")]
    public class TagController : Controller
    {
        private readonly ITagRepository _tagRepository;
        private readonly ITagService _tagService;
        private IMapper _mapper;
        private readonly ILogger<TagController> _logger;

        public TagController(ITagRepository tagRepository, ITagService tagService, IMapper mapper, ILogger<TagController> logger)
        {
            _tagRepository = tagRepository;
            _tagService = tagService;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// [Get] создание тега
        /// </summary>
        [Route("Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult AddTag()
        {
            return View();
        }

        /// <summary>
        /// [Post] создание тега
        /// </summary>
        [Route("Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> AddTag(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.AddTag(model);
                _logger.LogInformation($"Создан тег - {model.Name}");
                return RedirectToAction("Get", "Tag");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] редактирование тега
        /// </summary>
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> EditTag(Guid id)
        {
            try
            {
                var model = await _tagService.EditTag(id);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось получить тег по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Tag");
            }
        }

        /// <summary>
        /// [Post] редактирование тега
        /// </summary>
        [Route("Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model);
                _logger.LogInformation($"Изменен тег - {model.Name}");
                return RedirectToAction("Get", "Tag");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] удаление тега
        /// </summary>
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveTag(Guid id, bool isConfirm = true)
        {
            try
            {
                if (isConfirm)
                    await RemoveTag(id);
                return RedirectToAction("Get", "Tag");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить тег по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Tag");
            }
        }

        /// <summary>
        /// [Post] удаление тега
        /// </summary>
        [Route("Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.RemoveTag(id);
            _logger.LogDebug($"Удаленн тег - {id}");
            return RedirectToAction("Get", "Tag");
        }

        /// <summary>
        /// [Get] получение всех тегов
        /// </summary>
        [Route("Get")]
        [CustomAuthorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }
    }
}