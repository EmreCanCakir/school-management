using FluentValidation;
using Infrastructure.Utilities.Business;
using Infrastructure.Utilities.Results;
using UserManagement.DataAccess.Abstracts;
using UserManagement.Models;
using UserManagement.Services.ValidationRules;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace UserManagement.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailDal _userDetailDal;
        public UserDetailService(IUserDetailDal userDetailDal)
        {
            _userDetailDal = userDetailDal;
        }
        public async Task<IResult> Add(UserDetail entity)
        {
            await _userDetailDal.Add(entity);
            return new SuccessResult("User Created Successfully");
        }

        public Task<IResult> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserDetail>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<UserDetail> GetById(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
