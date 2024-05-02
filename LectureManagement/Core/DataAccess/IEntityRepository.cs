using System.Linq.Expressions;
using LectureManagement.Core.Entities;

namespace LectureManagement.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, IEntity, new()
    {
        List<T> GetAll(Expression<Func<T, bool>> filter = null);
    }
}
