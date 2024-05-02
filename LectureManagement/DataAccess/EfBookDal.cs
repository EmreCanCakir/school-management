using System.Linq.Expressions;
using LectureManagement.Core.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.DataAccess
{
    public class EfBookDal : EfEntityRepositoryBase<Book, MainDbContext>, IBookDal
    {
        public EfBookDal(MainDbContext context) : base(context)
        {
        }
    }
}
