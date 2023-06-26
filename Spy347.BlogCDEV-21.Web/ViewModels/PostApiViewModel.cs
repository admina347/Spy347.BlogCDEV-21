using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class PostApiViewModel
    {
        public Guid Id { get; set; } = new Guid();
        public string AuthorId { get; set; }
        //public List<TagViewModel> Tags { get; set; }


        [Required(ErrorMessage = "Поле Заголовок обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Заголовок", Prompt = "Заголовок")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Text { get; set; }
    }
}