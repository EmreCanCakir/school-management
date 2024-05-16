using Infrastructure.DataAccess.EntityFramework;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Concretes
{
    public class AcademicYearDal: EfEntityRepositoryBase<AcademicYear, MainDbContext>, IAcademicYearDal
    {
        public AcademicYearDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
