using Microsoft.AspNetCore.Identity;

namespace UserManagement.Models
{
    public class User: IdentityUser
    {
        public UserDetail UserDetail { get; set; }
    }
}
