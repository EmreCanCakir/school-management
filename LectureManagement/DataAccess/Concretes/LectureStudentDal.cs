using Infrastructure.DataAccess.EntityFramework;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Concretes
{
    public class LectureStudentDal: EfEntityRepositoryBase<LectureStudent, MainDbContext>, ILectureStudentDal
    {
        public LectureStudentDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
