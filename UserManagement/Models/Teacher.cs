namespace UserManagement.Models
{
    public class Teacher: UserDetail
    {
        public string? Title { get; set; }
        public string? Position { get; set; }
        public string EmployeeNumber { get; set; }
        public DateTime HireDate { get; set; }
        public decimal? Salary { get; set; }
        public TeacherStatus TeacherStatus { get; set; }
    }

    public enum TeacherStatus
    {
        Active,
        Inactive,
        Retired,
        OnLeave
    }
}
