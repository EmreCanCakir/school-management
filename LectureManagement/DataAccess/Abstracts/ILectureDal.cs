using Infrastructure.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Abstracts
{
    public interface ILectureDal: IEntityRepository<Lecture>
    {
    }
}
