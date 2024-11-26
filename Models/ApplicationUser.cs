using Microsoft.AspNetCore.Identity;

namespace FoodApp_project.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
        public string? Address { get; set; }
    }
}
