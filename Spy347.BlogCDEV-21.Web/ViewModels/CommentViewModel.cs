using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле Комментарий обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Комментарий", Prompt = "Комментарий")]
        public string Text { get; set; }

        public string AuthorEmail { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }     
           
    }
}