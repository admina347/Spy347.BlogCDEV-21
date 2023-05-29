using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Text { get; set; }

        public int? AuthorId { get; set; }
        //navigation properties
        public User Author { get; set; }

        // Навигационное свойство
        public int PostId { get; set; }
        public List<Post> Posts { get; set; }
    }
}