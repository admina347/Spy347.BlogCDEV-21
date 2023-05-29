using Spy347.BlogCDEV_21.Infrastructure.Data.Repository;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{

    public class CommentRepository : Repository<Comment>
    {
        public CommentRepository(ApplicationDbContext db) : base(db)
        {
           
        }

        /* public List<Comment> GetAllComments()
        {
            var comments = Set.AsEnumerable
            //return _context.Comments.ToList();
        }

        public Comment GetComment(int id)
        {
            return _context.Comments.FirstOrDefault(c => c.Id == id);
        }

        public List<Comment> GetCommentsByPostId(int id)
        {
            return _context.Comments.Where(c => c.PostId == id).ToList();
        }

        public async Task AddComment(Comment comment)
        {
            _context.Comments.Add(comment);
            await SaveChangesAsync();
        }

        public async Task UpdateComment(Comment comment)
        {
            _context.Comments.Update(comment);
            await SaveChangesAsync();
        }

        public async Task RemoveComment(int id)
        {
            _context.Comments.Remove(GetComment(id));
            await SaveChangesAsync();
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        } */
    }
}