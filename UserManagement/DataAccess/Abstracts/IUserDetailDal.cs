using Infrastructure.DataAccess;
using UserManagement.Models;

namespace UserManagement.DataAccess.Abstracts
{
    public interface IUserDetailDal: IEntityRepository<UserDetail>
    {
    }
}
