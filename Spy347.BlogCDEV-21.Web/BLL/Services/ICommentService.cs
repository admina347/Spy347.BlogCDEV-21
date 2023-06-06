using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface ICommentService
    {
        Task<Guid> CreateComment(CommentViewModel model, Guid UserId);
        Task<Guid> AddCommentFromPost(PostViewModel model, Guid userId);
        Task EditComment(CommentViewModel model);
        Task<List<Comment>> GetComments();
        Task RemoveComment(Guid id);
    }
}