using Infrastructure.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.DataAccess.Abstracts
{
    public interface ILectureStudentDal: IEntityRepository<LectureStudent>
    {
    }
}
