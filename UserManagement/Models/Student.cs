namespace UserManagement.Models
{
    public class Student: UserDetail
    {
        public string StudentNumber { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public string? DegreeProgram { get; set; }
        public StudentStatus StudentStatus { get; set; }
        public double? GPA { get; set; }
        public Guid AdvisorId { get; set; }
    }

    public enum StudentStatus
    {
        Active,
        Inactive,
        Graduated,
        OnLeave
    }
}
