using Infrastructure.DataAccess;
using LectureManagement.Model;
using System.Linq.Expressions;

namespace LectureManagement.DataAccess.Abstracts
{
    public interface ILectureDal : IEntityRepository<Lecture>
    {
       new Lecture Get(Expression<Func<Lecture, bool>> filter);
    }
}
