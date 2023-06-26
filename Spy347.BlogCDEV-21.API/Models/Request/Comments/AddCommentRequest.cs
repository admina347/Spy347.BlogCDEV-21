using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.API.Models.Request.Comments
{
    public class AddCommentRequest
    {
        public Guid Id { get; set; } = new Guid();

        [Required(ErrorMessage = "Поле Комментарий обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Комментарий", Prompt = "Комментарий")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Поле Автор обязательно для заполнения")]
        [DataType(DataType.EmailAddress)]
        public string AuthorEmail { get; set; }
        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }     

    }
}