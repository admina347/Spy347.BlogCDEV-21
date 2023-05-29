using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Post
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }

        public DateTime CreatedData { get; set; }

        public int? UserId { get; set; }
        //navigation properties
        public User User { get; set; }

        
        public List<Comment> Comments { get; set; }

        public List<Tag> Tags { get; set; }
        
    }
}