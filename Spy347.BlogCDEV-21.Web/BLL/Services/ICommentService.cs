using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public interface ICommentService
    {
        Task<int> CreateComment(CommentViewModel model, int UserId);
        Task EditComment(CommentViewModel model);
        Task<List<Comment>> GetComments();
        Task RemoveComment(int id);
    }
}