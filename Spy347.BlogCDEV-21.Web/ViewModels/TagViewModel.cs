using System.ComponentModel.DataAnnotations;
using Spy347.BlogCDEV_21.Infrastructure.Models;

namespace Spy347.BlogCDEV_21.Web.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [Display(Name = "Name")]
        public string Name { get; set; }
        public bool IsSelected { get; set; }

        public List<Post> Posts { get; set; }
    }
}