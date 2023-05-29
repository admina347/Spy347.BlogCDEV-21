using Microsoft.AspNetCore.Identity;

namespace Spy347.BlogCDEV_21.Infrastructure.Models
{
    public class Role : IdentityRole
    {
        //Id, Name, NormalizedName,  -> распологаются в классе родителя
        public int? SecurityLevel { get; set; } = null;
    }
}