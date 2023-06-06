using System.ComponentModel.DataAnnotations;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        public string Text { get; set; }
        public string AuthorId { get; set; }

        public string Comment { get; set; }

        public Guid UserId { get; set; }
        //navigation properties
        public User User { get; set; }

        public List<Comment> Comments { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}