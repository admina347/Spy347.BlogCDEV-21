using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels.Account
{
    public class UserEditViewModel
    {
        [DataType(DataType.Text)]
        [Display(Name = "Имя", Prompt = "Имя")]
        public string? FirstName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Фамилия", Prompt = "Фамилия")]
        public string? LastName { get; set; }

        [DataType(DataType.Text)]
        [Display(Name = "Никнейм", Prompt = "Никнейм")]
        public string? UserName { get; set; }

        [EmailAddress]
        [Display(Name = "Почта")]
        public string? Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Новый пароль")]
        public string? NewPassword { get; set; }

        [Display(Name = "Роли")]
        //public List<IdentiryRole> Roles { get; set; }

        public IEnumerable<Guid> Posts { get; set; }
        
        public List<RoleViewModel> Roles { get; set; }
        public IList<string> UserRoles { get; set; }
        public UserEditViewModel()
        {
            Roles = new List<RoleViewModel>();
            UserRoles = new List<string>();
        }

        public Guid Id { get; set; }
    }
}