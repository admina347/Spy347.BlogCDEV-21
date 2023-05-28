using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Comment
    {
        [Key]
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