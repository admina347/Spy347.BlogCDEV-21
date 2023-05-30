using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; }
        public string Text { get; set; }

        public Guid AuthorId { get; set; }
        //navigation properties
        public User Author { get; set; }

        // Навигационное свойство
        public Guid PostId { get; set; }
        public List<Post> Posts { get; set; }
    }
}