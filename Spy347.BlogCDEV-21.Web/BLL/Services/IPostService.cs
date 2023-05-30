using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface IPostService
    {
        Task<PostViewModel> CreatePost();
        Task<Guid> CreatePost(PostViewModel model);
        Task<PostViewModel> EditPost(Guid id);
        Task EditPost(PostViewModel model, Guid id);
        Task<List<Post>> GetPosts();
        Task RemovePost(Guid id);
        Task<Post> ShowPost(Guid id);
    }
}