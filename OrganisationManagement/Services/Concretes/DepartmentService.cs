using AutoMapper;
using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentDal _departmentDal;
        private readonly IMapper _mapper;

        public DepartmentService(IDepartmentDal departmentDal, IMapper mapper)
        {
            _departmentDal = departmentDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(DepartmentAddDto entity)
        {
            var department = _mapper.Map<Department>(entity);
            await _departmentDal.Add(department);
            return new SuccessResult("Department Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var entity = _departmentDal.Get(x=> x.Id == id);
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

        public async Task<IResult> Update(DepartmentUpdateDto entity)
        {
            var department = _mapper.Map<Department>(entity);
            await _departmentDal.Update(department);
            return new SuccessResult("Department Updated Successfully");
        }
    }
}
