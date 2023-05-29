using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface ITagService
    {
        Task<int> CreateTag(TagViewModel model);
        Task EditTag(TagViewModel model);
        Task<List<Tag>> GetTags();
        Task RemoveTag(int id);
    }
}