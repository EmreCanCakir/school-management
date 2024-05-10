using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;

        public DepartmentService(IDepartmentDal departmentDal)
        {
            _departmentDal = departmentDal;
        }

        public async Task<IResult> Add(Department entity)
        {
            await _departmentDal.Add(entity);
            return new SuccessResult("Department Created Successfully");
        }

        public async Task<IResult> Delete(Department entity)
        {
            await _departmentDal.Delete(entity);
            return new SuccessResult("Department Deleted Successfully");
        }

        public IDataResult<List<Department>> GetAll()
        {
            var result = _departmentDal.GetAll();
            return new SuccessDataResult<List<Department>>(result, "Departments Listed Successfully");
        }

        public IDataResult<Department> GetById(Guid id)
        {
            var result = _departmentDal.Get(x => x.Id == id);
            return new SuccessDataResult<Department>(result, "Department Get Successfully");
        }

        public async Task<IResult> Update(Department entity)
        {
            await _departmentDal.Update(entity);
            return new SuccessResult("Department Updated Successfully");
        }
    }
}
