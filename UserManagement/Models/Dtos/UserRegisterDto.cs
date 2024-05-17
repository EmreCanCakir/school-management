namespace UserManagement.Models.Dtos
{
    public class UserRegisterDto
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public long IdentityNumber { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public string DepartmentId { get; set; }
        public string FacultyId { get; set; }
        public string? UserId { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
    }

    public class LecturerRegisterDto : UserRegisterDto
    {
        public UserType UserType { get; set; } = Models.UserType.Lecturer;
        public string Title { get; set; }
        public int PositionId { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public LecturerStatus LecturerStatus { get; set; } = Models.LecturerStatus.Active;
    }

    public class StudentRegisterDto : UserRegisterDto
    {
        public UserType UserType { get; set; } = Models.UserType.Student;
        public string StudentNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string DegreeProgram { get; set; }
        public StudentStatus StudentStatus { get; set; } = Models.StudentStatus.Active;
        public Guid AdvisorId { get; set; }
    }
}
