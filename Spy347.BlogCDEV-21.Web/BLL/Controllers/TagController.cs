using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Controllers
{
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
        [Route("Tag/Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult CreateTag()
        {
            return View();
        }

        /// <summary>
        /// [Post] создание тега
        /// </summary>
        [Route("Tag/Add")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> CreateTag(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                var tagId = _tagService.CreateTag(model);
                _logger.LogInformation($"Создан тег - {model.Name}");
                return RedirectToAction("Index", "Home");
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
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public IActionResult EditTag(Guid id)
        {
            var view = new TagViewModel { Id = id};
            return View(view);
        }

        /// <summary>
        /// [Post] редактирование тега
        /// </summary>
        [Route("Tag/Edit")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> EditTag(TagViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _tagService.EditTag(model);
                _logger.LogDebug($"Изменен тег - {model.Name}");
                return RedirectToAction("Index", "Home");
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
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> RemoveTag(Guid id, bool isConfirm = true)
        {
            if (isConfirm)
                await RemoveTag(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] удаление тега
        /// </summary>
        [Route("Tag/Remove")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpPost]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagService.RemoveTag(id);
            _logger.LogDebug($"Удаленн тег - {id}");
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] получение всех тегов
        /// </summary>
        [Route("Tag/Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetTags()
        {
            var tags = await _tagService.GetTags();
            return View(tags);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}