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
        [Route("Comment/Add")]
        public IActionResult CreateComment(Guid postId)
        {
            var model = new CommentViewModel() { PostId = postId };
            return View(model);
        }

        // <summary>
        /// [Post] добавление комментария
        /// </summary>
        [HttpPost]
        [Route("Comment/Add")]
        public async Task<IActionResult> CreateComment(CommentViewModel model, Guid postId)
        {
            model.PostId = postId;
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = _commentService.CreateComment(model, new Guid(user.Id));
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] редактирования коментария
        /// </summary>
        [Route("Comment/Edit")]
        [HttpGet]
        public IActionResult EditComment(Guid id)
        {
            var view = new CommentViewModel { Id = id };
            return View(view);
        }

        /// <summary>
        /// [Post] редактирования коментария
        /// </summary>
        [Authorize]
        [Route("Comment/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditComment(CommentViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _commentService.EditComment(model);
                return RedirectToAction("Index", "Home");
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
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Delete] удаления коментария
        /// </summary>
        [HttpDelete]
        [Route("Comment/Remove")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);
            return RedirectToAction("Index", "Home");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}