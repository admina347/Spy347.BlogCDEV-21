using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class RoleViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "Поле Название обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Название", Prompt = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Поле Описание обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Описание", Prompt = "Описание")]
        public string Description { get; set; }


        [Required(ErrorMessage = "Уровень доступа обязательно для заполнения")]
        [DataType(DataType.Text)]
        [Display(Name = "Уровень доступа", Prompt = "Уровень")]
        public int SecurityLevel { get; set; }

        public bool IsSelected { get; set; }
    }
}