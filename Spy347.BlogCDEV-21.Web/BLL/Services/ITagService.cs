using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface ITagService
    {
        Task<Guid> AddTag(TagViewModel model);
        Task EditTag(TagViewModel model);
        Task<TagViewModel> EditTag(Guid id);
        Task<List<Tag>> GetTags();
        Task<List<Tag>> GetTagsApi();
        Task RemoveTag(Guid id);
    }
}