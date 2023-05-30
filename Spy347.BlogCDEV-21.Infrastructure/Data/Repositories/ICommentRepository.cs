using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Infrastructure.Repositories
{
    public interface ICommentRepository
    {
        Task AddComment(Comment comment);
        List<Comment> GetAllComments();
        Comment GetComment(Guid id);
        List<Comment> GetCommentsByPostId(Guid id);
        Task RemoveComment(Guid id);
        Task<bool> SaveChangesAsync();
        Task UpdateComment(Comment comment);
    }
}