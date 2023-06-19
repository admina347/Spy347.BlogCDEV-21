using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Controllers
{
    [Route("[controller]")]
    public class CommentController : Controller
    {
        private ICommentRepository _commentRepository;
        private ICommentService _commentService;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly ILogger<CommentController> _logger;

        public CommentController(ICommentRepository commentRepository, ICommentService commentService, UserManager<User> userManager, IMapper mapper,  ILogger<CommentController> logger)
        {
            _commentRepository = commentRepository;
            _commentService = commentService;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        // <summary>
        /// [Get] добавление комментария
        /// </summary>
        [HttpGet]
        [Route("Add")]
        public IActionResult AddComment(Guid postId)
        {
            var model = new CommentViewModel() { PostId = postId };
            return View(model);
        }

        // <summary>
        /// [Post] добавление комментария
        /// </summary>
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComment(CommentViewModel model, Guid postId)
        {
            model.PostId = postId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = _commentService.CreateComment(model, new Guid(user.Id));
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] редактирования коментария
        /// </summary>
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> EditComment(Guid id)
        {
            try
            {
                var model = await _commentService.EditComment(id);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось получить комментарий по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Comment");
            }
        }

        /// <summary>
        /// [Post] редактирования коментария
        /// </summary>
        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                return RedirectToAction("Get", "Comment");
            }
            else
            {
                ModelState.AddModelError("", "Некорректные данные");
                return View(model);
            }
        }

        /// <summary>
        /// [Get] удаления коментария
        /// </summary>
        [HttpGet]
        [Route("Remove")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            try
            {
                if (confirm)
                    await RemoveComment(id);
                return RedirectToAction("Get", "Comment");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить комментарий по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Comment");
            }
        }

        /// <summary>
        /// [Delete] удаления коментария
        /// </summary>
        [HttpDelete]
        [Route("Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            return RedirectToAction("Get", "Comment");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        /// <summary>
        /// [Get] получение всех комментариев
        /// </summary>
        [Route("Get")]
        [Authorize(Roles = "Администратор, Модератор")]
        [HttpGet]
        public async Task<IActionResult> GetComments()
        {
            var comments = await _commentService.GetComments();
            return View(comments);
        }

    }
}