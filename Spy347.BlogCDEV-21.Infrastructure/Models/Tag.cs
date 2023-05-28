using System.ComponentModel.DataAnnotations;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Post> Posts { get; set; }
    }
}