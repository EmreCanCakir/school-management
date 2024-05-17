using Infrastructure.DataAccess.EntityFramework;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Concretes
{
    public class LectureScheduleDal: EfEntityRepositoryBase<LectureSchedule, MainDbContext>, ILectureScheduleDal
    {
        public LectureScheduleDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
