using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public class CommentService : ICommentService
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

        public async Task<Guid> CreateComment(CommentViewModel model, Guid userId)
        {
            var author = await _userManager.FindByIdAsync(userId.ToString());
            
            Comment comment = new Comment
            {
                //Title = model.Title,
                Text = model.Text,
                AuthorEmail = author.Email,
                PostId = model.PostId,
                AuthorId = userId,
                //realAuthorName = _userManager.FindByIdAsync(UserId.ToString()).Result.UserName,
            };

            await _commentRepository.AddComment(comment);
            return comment.Id;
        }

        public async Task<Guid> AddCommentFromPost(PostViewModel model, Guid userId)
        {
            var author = await _userManager.FindByIdAsync(userId.ToString());
            
            Comment comment = new Comment
            {
                Text = model.Comment,
                AuthorEmail = author.Email,
                PostId = model.Id,
                AuthorId = userId,
            };

            await _commentRepository.AddComment(comment);
            return comment.Id;
        }

        public async Task<CommentViewModel> EditComment(Guid id)
        {
            var comment = _commentRepository.GetComment(id);

            var model = new CommentViewModel()
            {
                Id = id,
                Text = comment.Text,
                AuthorEmail = comment.AuthorEmail
            };

            return model;
        }

        public async Task EditComment(CommentViewModel model)
        {
            var comment = _commentRepository.GetComment(model.Id);

            comment.Text = model.Text;
            comment.AuthorEmail = model.AuthorEmail;

            await _commentRepository.UpdateComment(comment);
        }

        public async Task RemoveComment(Guid id)
        {
            await _commentRepository.RemoveComment(id);
        }

        public async Task<List<Comment>> GetComments()
        {
            return _commentRepository.GetAllComments().ToList();
        }
    }
}