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
    public class CommentController : Controller
    {
         private readonly ICommentService _commentService;
        private readonly IMapper _mapper;

        public CommentController(ICommentService commentService, IMapper mapper)
        {
            _commentService = commentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех комментарий поста
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetPostComment")]
        public async Task<IEnumerable<Comment>> GetPostComments(Guid id)
        {
            var comment = await _commentService.GetComments();
            return comment.Where(c => c.PostId == id);
        }

        /// <summary>
        /// Создания комментария к посту
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddComment(CommentViewModel request, Guid userId)
        {
            var result = await _commentService.CreateComment(request, userId);
            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование комментария
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPut]
        [Route("Edit/{id}")]
        public async Task<IActionResult> EditComment(
            [FromRoute] Guid id,
            [FromBody] CommentViewModel request)
        {
            var result = await _commentService.EditCommentApi(request);
            if (result == 1)
                return StatusCode(201);
            else
                return StatusCode(204);
        }

        /// <summary>
        /// Удаление комментария
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("Remove/{id}")]
        public async Task<IActionResult> RemoveComment(Guid id)
        {
            await _commentService.RemoveComment(id);

            return StatusCode(201);
        }
    }
}