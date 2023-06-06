using Microsoft.EntityFrameworkCore;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{

    public class PostRepository : IPostRepository
    {
        private ApplicationDbContext _context;
        public PostRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Post> GetAllPosts()
        {
            return _context.Posts.Include(p => p.Tags).ToList();
        }

        public Post GetPost(Guid id)
        {
            return _context.Posts.Include(u => u.User).Include(p => p.Tags).Include(c => c.Comments).FirstOrDefault(p => p.Id == id);

            /* return _context.Posts.Include(p => p.Tags)
            
            .Include(u => u.User)
            .Include(c => c.Comments)
            .ThenInclude(c => c.Author).ToList()
            //.Include(c => c.Comments.Select(a => ((User)a.Author))).ToList()
            
            .FirstOrDefault(p => p.Id == id); */
            //.ThenInclude(c => c.Author) - not work
        }

        public async Task AddPost(Post post)
        {
            _context.Posts.Add(post);
            await SaveChangesAsync();
        }

        public async Task UpdatePost(Post post)
        {
            _context.Posts.Update(post);
            await SaveChangesAsync();
        }

        public async Task RemovePost(Guid id)
        {
            var post = GetPost(id);
            if (post != null)
            {
                _context.Posts.Remove(post);
                await SaveChangesAsync();
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }
    }
}