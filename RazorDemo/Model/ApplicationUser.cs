using Microsoft.AspNetCore.Identity;

namespace RazorPageDemo.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string Address { get; set; }
        public string Role { get; internal set; }

        // Additional properties for your user entity, if any
    }
}
