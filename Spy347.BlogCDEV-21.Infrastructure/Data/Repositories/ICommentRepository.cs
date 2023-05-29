using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        List<Comment> GetAllComments();
        Comment GetComment(int id);
        List<Comment> GetCommentsByPostId(int id);
        Task RemoveComment(int id);
        Task<bool> SaveChangesAsync();
        Task UpdateComment(Comment comment);
    }
}