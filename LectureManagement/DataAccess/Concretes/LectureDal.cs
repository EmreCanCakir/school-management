using Infrastructure.DataAccess.EntityFramework;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Concretes
{
    public class LectureDal: EfEntityRepositoryBase<Lecture, MainDbContext>, ILectureDal
    {
        public LectureDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
