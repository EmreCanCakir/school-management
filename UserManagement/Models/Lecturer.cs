namespace UserManagement.Models
{
    public class Lecturer: UserDetail
    {
        public string? Title { get; set; }
        public int PositionId { get; set; }
#nullable disable
        public string EmployeeNumber { get; set; }
#nullable enable
        public DateTime HireDate { get; set; }
        public decimal? Salary { get; set; }
        public LecturerStatus LecturerStatus { get; set; }
    }

    public enum LecturerStatus
    {
        Active,
        Inactive,
        Retired,
        OnLeave
    }
}
