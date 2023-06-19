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
    public class PostController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly IPostService _postService;
        private readonly ICommentService _commentService;
        private readonly ITagRepository _tagRepository;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, IPostService postService, ICommentService commentService, ITagRepository tagRepository, UserManager<User> userManager, IMapper mapper, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _postService = postService;
            _commentService = commentService;
            _tagRepository = tagRepository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// [Get] показ поста
        /// </summary>
        [Route("Show/{id?}")]
        [HttpGet]
        public async Task<IActionResult> ShowPost(Guid id)
        {
            try
            {
                var post = await _postService.ShowPost(id);
                return View(post);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось получить статью по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Post");
            }
        }

        /// <summary>
        /// [Get] создания поста
        /// </summary>
        [Route("Add")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AddPost()
        {
            var model = await _postService.CreatePost();

            return View(model);
        }

        /// <summary>
        /// [Post] создания поста
        /// </summary>
        [Route("Add")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> AddPost(PostViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.AuthorId = user.Id;

            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            var postId = await _postService.CreatePost(model);
            _logger.LogInformation($"Добавлена статья - {model.Title}");
            return RedirectToAction("Get", "Post");
        }

        /// <summary>
        /// [Get] редактирования поста
        /// </summary>
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            try
            {
                var model = await _postService.EditPost(id);
    
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось получить статью по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Post");
            }
        }

        /// <summary>
        /// [Post] редактирования поста
        /// </summary>
        [Authorize]
        [Route("Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            await _postService.EditPost(model, Id);
            _logger.LogDebug($"Изменена статья - {model.Title}");
            return RedirectToAction("Get", "Post");
        }

        /// <summary>
        /// [Get] удаления поста
        /// </summary>
        [HttpGet]
        [Route("Remove")]
        public async Task<IActionResult> RemovePost(Guid id, bool confirm = true)
        {
            try
            {
                if (confirm)
                    await RemovePost(id);
                return RedirectToAction("Get", "Post");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить статью по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Post");
            }
        }

        /// <summary>
        /// [Post] удаления поста
        /// </summary>
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            try
            {
                await _postService.RemovePost(id);  //Почему-то не вознивает ошибка если нет id, потом что есть проверка в методе удаления
                _logger.LogDebug($"Удалена статья - {id}");
                return RedirectToAction("Get", "Post");
            }
            catch (Exception ex)
            {
                _logger.LogDebug($"Ошибка: {ex}");
                _logger.LogError($"Ошибка: не удалось удалить статью по id - {id} for more information see information log.");
                return RedirectToAction("Get", "Post");
            }
        }

        /// <summary>
        /// [Get] получить все посты
        /// </summary>
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            return View(posts);
        }

        // <summary>
        /// [Post] добавление комментария
        /// </summary>
        [HttpPost]
        [Route("AddComment")]
        public async Task<IActionResult> AddComment(PostViewModel model, Guid postId)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var post = _commentService.AddCommentFromPost(model, new Guid(user.Id));
            return RedirectToAction("Show", "Post", new {id = postId});
        }
    }
}