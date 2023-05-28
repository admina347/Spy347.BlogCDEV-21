using Microsoft.AspNetCore.Identity;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class User : IdentityUser
    {
        //Id, FirstName, LastName, UserName, Email -> распологаются в классе родителя

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedData { get; set; } = DateTime.Now;

    }
}