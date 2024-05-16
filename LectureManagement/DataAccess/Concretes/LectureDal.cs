using Infrastructure.DataAccess.EntityFramework;
using Infrastructure.Entities.Abstracts;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace LectureManagement.DataAccess.Concretes
{
    public class LectureDal: EfEntityRepositoryBase<Lecture, MainDbContext>, ILectureDal
    {
        private readonly MainDbContext _context;
        public LectureDal(MainDbContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        Lecture ILectureDal.Get(Expression<Func<Lecture, bool>> filter)
        {
            return _context.Set<Lecture>()
                .Include(x => x.Prerequisites)
                .FirstOrDefault(filter);
        }
    }
}
