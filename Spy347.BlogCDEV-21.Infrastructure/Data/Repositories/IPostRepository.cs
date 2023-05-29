using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{
    public interface IPostRepository
    {
        Task AddPost(Post post);
        List<Post> GetAllPosts();
        Post GetPost(int id);
        Task RemovePost(int id);
        Task<bool> SaveChangesAsync();
        Task UpdatePost(Post post);
    }
}