using Infrastructure.Business;
using UserManagement.Models;

namespace UserManagement.Services
{
    public interface IUserDetailService: IBaseService<UserDetail, Guid>
    {
    }
}
