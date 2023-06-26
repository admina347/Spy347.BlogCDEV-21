using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.BLL.Services;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IMapper _mapper;

        public PostController(IPostService postService, IMapper mapper)
        {
            _postService = postService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех постов
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetPosts")]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            var posts = await _postService.GetPosts();
            //var postsResponse = posts.Select(p => new PostViewModel { Id = p.Id, AuthorId = p.AuthorId, Title = p.Title, Text = p.Text, Tags = p.Tags.Select(_ => _.Name)}).ToList();

            return posts;   //postsResponse;
        }

        /// <summary>
        /// Добавление поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("AddPost")]
        public async Task<IActionResult> AddPost(PostApiViewModel request)
        {
            var result = await _postService.CreatePostApi(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("EditPost")]
        public async Task<IActionResult> EditPost(PostApiViewModel request)
        {
            await _postService.EditPostApi(request, request.Id);

            return StatusCode(201);
        }

        /// <summary>
        /// Удаление поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemovePost")]
        public async Task<IActionResult> RemovePost(Guid id)
        {
            await _postService.RemovePost(id);

            return StatusCode(201);
        }
    }
}