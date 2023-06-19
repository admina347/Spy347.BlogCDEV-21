using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Название тега", Prompt = "Тег")]
        public string Name { get; set; }
        public bool IsSelected { get; set; }
    }
}