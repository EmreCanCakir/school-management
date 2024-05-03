using System.ComponentModel.DataAnnotations;

namespace UserManagement.Models
{
    public class UserDetail
    {
        #nullable disable
        [Key]
        public string Id { get; set; }
        #nullable enable
        public long IdentityNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string Department { get; set; }
        public string Faculty { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
