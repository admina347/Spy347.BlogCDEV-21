using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Post
    {
        [Key]
        public Guid Id { get; set; } = Guid.NewGuid();
        [Required]
        [MaxLength(100)]
        public string Title { get; set; } = String.Empty;
        public string Text { get; set; } = String.Empty;
        public string AuthorId { get; set; } = String.Empty;

        public DateTime CreatedData { get; set; } = DateTime.Now;

        public Guid UserId { get; set; }
        //navigation properties
        public User User { get; set; }
        public List<Comment> Comments { get; set; } = new List<Comment>();
        public List<Tag> Tags { get; set; }
        
    }
}