using System.ComponentModel.DataAnnotations;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class CommentViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле Заголовок обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Text { get; set; }

        [Required(ErrorMessage = "Поле Автор обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Автор", Prompt = "Автор")]

        public Guid AuthorId { get; set; }
        public Guid PostId { get; set; }     
           
    }
}