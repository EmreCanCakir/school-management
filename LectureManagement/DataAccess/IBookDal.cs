using LectureManagement.Core.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.DataAccess
{
    public interface IBookDal: IEntityRepository<Book>
    {
    }
}
