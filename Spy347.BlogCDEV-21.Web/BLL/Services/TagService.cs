using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public class TagService : ITagService
    {
        [BindProperty]
        public TagViewModel TagViewModel { get; set; }
        private readonly ITagRepository _tagRepository;
        private IMapper _mapper;


        public TagService(ITagRepository tagRepository, IMapper mapper)
        {
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<Guid> AddTag(TagViewModel model)
        {
            var tag = _mapper.Map<Tag>(model);
            await _tagRepository.AddTag(tag);

            return tag.Id;
        }

        public async Task EditTag(TagViewModel model)
        {
            if (string.IsNullOrEmpty(model.Name))
                return;

            var tag = await _tagRepository.GetTag(model.Id);
            tag.Name = model.Name;
            await _tagRepository.UpdateTag(tag);
        }
        public async Task<TagViewModel> EditTag(Guid id)
        {

            var tag = await _tagRepository.GetTag(id);

            var model = new TagViewModel()
            {
                Id = id,
                Name = tag.Name
            };

            return model;
        }


        public async Task RemoveTag(Guid id)
        {
            await _tagRepository.RemoveTag(id);
        }

        public async Task<List<Tag>> GetTags()
        {
            return _tagRepository.GetAllTags().ToList();
        }
    }
}