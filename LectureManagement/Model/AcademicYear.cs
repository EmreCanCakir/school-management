using Infrastructure.Entities.Abstracts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LectureManagement.Model
{
    public class AcademicYear: IEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public Guid Id { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public AcademicYearStatus Status { get; set; }
    }

    public enum AcademicYearStatus
    {
        Active = 1,
        Inactive = 2
    }
}
