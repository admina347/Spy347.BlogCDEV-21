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
    public class TagController : Controller
    {
        private readonly ITagService _tagSerive;
        private readonly IMapper _mapper;

        public TagController(ITagService tagService, IMapper mapper)
        {
            _tagSerive = tagService;
            _mapper = mapper;
        }

        /// <summary>
        /// Получение всех тегов
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpGet]
        [Route("GetTags")]
        public async Task<IEnumerable<Tag>> GetTags()
        {
            var tags = await _tagSerive.GetTags();
            // A possible object cycle was detected. This can either be due to a cycle or if the object depth is larger than the maximum allowed depth of 32.
            //var tags = await _tagSerive.GetTagsApi();
            return tags;
        }

        /// <summary>
        /// Добавление тега
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPost]
        [Route("AddTag")]
        public async Task<IActionResult> AddTag(TagViewModel request)
        {
            var result = await _tagSerive.AddTag(request);
            return StatusCode(201);
        }

        /// <summary>
        /// Редактирование тега
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpPatch]
        [Route("EditTag")]
        public async Task<IActionResult> EditTag(TagViewModel request)
        {
            await _tagSerive.EditTag(request);

            return StatusCode(201);
        }

        /// <summary>
        /// Удаление тега
        /// </summary>
        [Authorize(Roles = "Администратор")]
        [HttpDelete]
        [Route("RemoveTag")]
        public async Task<IActionResult> RemoveTag(Guid id)
        {
            await _tagSerive.RemoveTag(id);

            return StatusCode(201);
        }
    }
}