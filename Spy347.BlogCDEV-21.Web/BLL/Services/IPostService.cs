using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface IPostService
    {
        Task<PostViewModel> CreatePost();
        Task<int> CreatePost(PostViewModel model);
        Task<PostViewModel> EditPost(int id);
        Task EditPost(PostViewModel model, int id);
        Task<List<Post>> GetPosts();
        Task RemovePost(int id);
        Task<Post> ShowPost(int id);
    }
}