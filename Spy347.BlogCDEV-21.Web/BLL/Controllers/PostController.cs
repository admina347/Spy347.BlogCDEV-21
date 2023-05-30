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
        private readonly ITagRepository _tagRepository;
        private readonly UserManager<User> _userManager;
        private IMapper _mapper;
        private readonly ILogger<PostController> _logger;

        public PostController(IPostRepository postRepository, IPostService postService, ITagRepository tagRepository, UserManager<User> userManager, IMapper mapper, ILogger<PostController> logger)
        {
            _postRepository = postRepository;
            _postService = postService;
            _tagRepository = tagRepository;
            _userManager = userManager;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// [Get] показ поста
        /// </summary>
        [Route("Post/Show")]
        [HttpGet]
        public async Task<IActionResult> ShowPost(Guid id)
        {
            var post = await _postService.ShowPost(id);
            return View(post);
        }

        /// <summary>
        /// [Get] создания поста
        /// </summary>
        [Route("Post/Add")]
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> CreatePost()
        {
            var model = await _postService.CreatePost();

            return View(model);
        }

        /// <summary>
        /// [Post] создания поста
        /// </summary>
        [Route("Post/Add")]
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreatePost(PostViewModel model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            model.AuthorId = user.Id;

            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            var postId = await _postService.CreatePost(model);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] редактирования поста
        /// </summary>
        [Route("Post/Edit")]
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
        [Route("Post/Edit")]
        [HttpPost]
        public async Task<IActionResult> EditPost(PostViewModel model, Guid Id)
        {
            if (string.IsNullOrEmpty(model.Title) || string.IsNullOrEmpty(model.Text))
            {
                ModelState.AddModelError("", "Не все поля заполненны");
                return View(model);
            }

            await _postService.EditPost(model, Id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] удаления поста
        /// </summary>
        [HttpGet]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id, bool confirm = true)
        {
            if (confirm)
                await RemovePost(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Post] удаления поста
        /// </summary>
        [HttpPost]
        [Route("Post/Remove")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// [Get] получить все посты
        /// </summary>
        [HttpGet]
        [Route("Post/Get")]
        public async Task<IActionResult> GetPosts()
        {
            var posts = await _postService.GetPosts();

            return View(posts);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}