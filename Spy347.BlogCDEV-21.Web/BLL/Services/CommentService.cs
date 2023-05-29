using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public class CommentService
    {
        public IMapper _mapper;
        private ICommentRepository _commentRepository;
        private UserManager<User> _userManager;

        public CommentService(IMapper mapper, ICommentRepository commentRepository, UserManager<User> userManager)
        {
            _mapper = mapper;
            _commentRepository = commentRepository;
            _userManager = userManager;
        }

        public async Task<int> CreateComment(CommentViewModel model, int UserId)
        {
            Comment comment = new Comment
            {
                Title = model.Title,
                Text = model.Text,
                //Author = model.Author,
                PostId = model.PostId,
                AuthorId = UserId,
                //realAuthorName = _userManager.FindByIdAsync(UserId.ToString()).Result.UserName,
            };

            await _commentRepository.AddComment(comment);
            return comment.Id;
        }

        public async Task EditComment(CommentViewModel model)
        {
            var comment = _commentRepository.GetComment(model.Id);

            comment.Title = model.Title;
            comment.Text = model.Text;
            //comment.Author = model.Author;

            await _commentRepository.UpdateComment(comment);
        }

        public async Task RemoveComment(int id)
        {
            await _commentRepository.RemoveComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return _commentRepository.GetAllComments().ToList();
        }
    }
}