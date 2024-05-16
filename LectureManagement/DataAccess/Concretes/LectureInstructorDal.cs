using Infrastructure.DataAccess.EntityFramework;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Concretes
{
    public class LectureInstructorDal: EfEntityRepositoryBase<LectureInstructor, MainDbContext>, ILectureInstructorDal
    {
        public LectureInstructorDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
