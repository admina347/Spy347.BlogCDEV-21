using System.ComponentModel.DataAnnotations;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class PostViewModel
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Text { get; set; }

        public DateTime CreatedData { get; set; }

        public int? UserId { get; set; }
        //navigation properties
        public User User { get; set; }

        public List<Comment> Comments { get; set; }
        public List<Tag> Tags { get; set; }
    }
}