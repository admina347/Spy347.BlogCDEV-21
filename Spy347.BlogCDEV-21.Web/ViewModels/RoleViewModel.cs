using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class RoleViewModel
    {
        public string Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }

        public bool IsSelected { get; set; }
    }
}