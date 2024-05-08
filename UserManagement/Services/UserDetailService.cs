using FluentValidation;
using Infrastructure.Utilities.Business;
using Infrastructure.Utilities.Results;
using UserManagement.DataAccess.Abstracts;
using UserManagement.Models;
using UserManagement.Services.ValidationRules;

namespace UserManagement.Services
{
    public class UserDetailService : IUserDetailService
    {
        private readonly IUserDetailDal _userDetailDal;
        public UserDetailService(IUserDetailDal userDetailDal)
        {
            _userDetailDal = userDetailDal;
        }
        public async Task<Infrastructure.Utilities.Results.IResult> Add(UserDetail entity)
        {
            await _userDetailDal.Add(entity);
            return new SuccessResult("User Created Successfully");
        }

        public async Task<Infrastructure.Utilities.Results.IResult> Delete(UserDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<UserDetail>>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<UserDetail>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Infrastructure.Utilities.Results.IResult> Update(UserDetail entity)
        {
            throw new NotImplementedException();
        }
    }
}
