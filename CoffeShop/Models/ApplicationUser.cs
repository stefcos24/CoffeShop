using Microsoft.AspNetCore.Identity;

namespace CoffeShop.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
