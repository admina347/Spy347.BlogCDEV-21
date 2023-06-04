using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Spy347.BlogCDEV_21.Infrastructure.Models;
using Spy347.BlogCDEV_21.Infrastructure.Repositories;
using Spy347.BlogCDEV_21.Web.ViewModels;

namespace Spy347.BlogCDEV_21.Web.BLL.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly ITagRepository _tagRepository;
        private readonly UserManager<User> _userManager;
        private readonly ICommentRepository _commentRepo;
        private IMapper _mapper;

        CommentViewModel commentViewModel = new CommentViewModel();


        public PostService(ITagRepository tagRepository, IPostRepository postRepository, IMapper mapper, UserManager<User> userManager, ICommentRepository commentRepo)
        {
            _postRepository = postRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
            _userManager = userManager;
            _commentRepo = commentRepo;
        }

        public async Task<PostViewModel> CreatePost()
        {
            Post post = new Post();

            var allTags = _tagRepository.GetAllTags().Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();

            PostViewModel model = new PostViewModel
            {
                Title = post.Title = string.Empty,
                Text = post.Text = string.Empty,
                Tags = allTags
            };

            return model;
        }

        public async Task<Guid> CreatePost(PostViewModel model)
        {
            var dbTags = new List<Tag>();

            if (model.Tags != null)
            {
                var postTags = model.Tags.Where(t => t.IsSelected == true).ToList();
                var tagsId = postTags.Select(t => t.Id).ToList();
                dbTags = _tagRepository.GetAllTags().Where(t => tagsId.Contains(t.Id)).ToList();
            }

            Post post = new Post
            {
                Id = model.Id,
                Title = model.Title,
                Text = model.Text,
                Tags = dbTags,
                AuthorId = model.AuthorId
            };

            var user = await _userManager.FindByIdAsync(model.AuthorId);
            user.Posts.Add(post);

            await _postRepository.AddPost(post);
            await _userManager.UpdateAsync(user);

            return post.Id;
        }

        public async Task<PostViewModel> EditPost(Guid id)
        {
            var post = _postRepository.GetPost(id);

            var tags = _tagRepository.GetAllTags().Select(t => new TagViewModel() { Id = t.Id, Name = t.Name }).ToList();

            foreach (var tag in tags)
            {
                foreach (var postTag in post.Tags)
                {
                    if (postTag.Id == tag.Id)
                    {
                        tag.IsSelected = true;
                        break;
                    }
                }
            }

            var model = new PostViewModel()
            {
                Id = id,
                Title = post.Title,
                Text = post.Text,
                Tags = tags
            };

            return model;
        }

        public async Task EditPost(PostViewModel model, Guid id)
        {
            var post = _postRepository.GetPost(id);

            post.Title = model.Title;
            post.Text = model.Text;

            foreach (var tag in model.Tags)
            {
                var tagChanged = _tagRepository.GetTag(tag.Id);
                if (tag.IsSelected)
                {
                    post.Tags.Add(tagChanged);
                }
                else
                {
                    post.Tags.Remove(tagChanged);
                }
            }

            await _postRepository.UpdatePost(post);
        }

        public async Task RemovePost(Guid id)
        {
            await _postRepository.RemovePost(id);
        }

        public async Task<List<Post>> GetPosts()
        {
            var posts = _postRepository.GetAllPosts().ToList();

            return posts;
        }

        public async Task<Post> ShowPost(Guid id)
        {
            var post = _postRepository.GetPost(id);
            //var user = await _userManager.FindByIdAsync(post.AuthorId.ToString());
            //var author = _userManager.FindByIdAsync(post.AuthorId.ToString());
            //var comments = _commentRepo.GetCommentsByPostId(post.Id);
            //post.Id = id;
            

            /* foreach (var comment in post.Comments)
            {
                if (post.Comments.FirstOrDefault(c => c.Id == comment.Id) == null)
                {
                    post.Comments.Add(comment);
                }
            } */
            /* Console.WriteLine("Comments:" + post.Comments.Count());
            for (int i = 1; i < post.Comments.Count(); i++)
            {
                var author = await _userManager.FindByIdAsync(post.Comments[i].AuthorId.ToString());
                if (author != null)
                post.Comments[i].Author.Email = "test";         //author.Email;
                Console.WriteLine("Comment - " + i);
            } */

            /* if (!string.IsNullOrEmpty(user.UserName))
            {
                post.AuthorId = user.UserName;
            }
            else
            {
                post.AuthorId = "nonUsernamed";
            } */

            return post;
        }

        
    }
}