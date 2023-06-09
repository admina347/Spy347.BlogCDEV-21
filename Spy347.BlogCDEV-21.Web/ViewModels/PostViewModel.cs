using System.ComponentModel.DataAnnotations;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class PostViewModel
    {
        public Guid Id { get; set; }
        
        [Required]
        [MaxLength(100)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }
        [Required]
        [Display(Name = "Текст", Prompt = "Текст")]
        public string Text { get; set; }
        public string AuthorId { get; set; }

        
        [DataType(DataType.Text)]
        [Display(Name = "Комментарий", Prompt = "Комментарий")]
        public string? Comment { get; set; }

        public Guid UserId { get; set; }
        //navigation properties
        public User User { get; set; }

        public List<Comment> Comments { get; set; }
        public List<TagViewModel> Tags { get; set; }
    }
}