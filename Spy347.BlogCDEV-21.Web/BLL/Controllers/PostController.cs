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
            var post = await _postService.ShowPost(id);
            return View(post);
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
            return RedirectToAction("Get", "Post");
        }

        /// <summary>
        /// [Get] редактирования поста
        /// </summary>
        [Route("Edit")]
        [HttpGet]
        public async Task<IActionResult> EditPost(Guid id)
        {
            var model = await _postService.EditPost(id);

            return View(model);
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
            return RedirectToAction("Get", "Post");
        }

        /// <summary>
        /// [Get] удаления поста
        /// </summary>
        [HttpGet]
        [Route("Remove")]
        public async Task<IActionResult> RemovePost(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemovePost(id);
            return RedirectToAction("Get", "Post");
        }

        /// <summary>
        /// [Post] удаления поста
        /// </summary>
        [HttpPost]
        [Route("Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);
            return RedirectToAction("Get", "Post");
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


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}