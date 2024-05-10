using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyDal _facultyDal;

        public FacultyService(IFacultyDal facultyDal)
        {
            _facultyDal = facultyDal;
        }

        public async Task<IResult> Add(Faculty entity)
        {
            await _facultyDal.Add(entity);
            return new SuccessResult("Faculty Created Successfully");
        }

        public async Task<IResult> Delete(Faculty entity)
        {
            await _facultyDal.Delete(entity);
            return new SuccessResult("Faculty Deleted Successfully");
        }

        public IDataResult<List<Faculty>> GetAll()
        {
            var result = _facultyDal.GetAll();
            return new SuccessDataResult<List<Faculty>>(result, "Faculties Listed Successfully");
        }

        public IDataResult<Faculty> GetById(Guid id)
        {
            var result = _facultyDal.Get(x=> x.Id == id);
            return new SuccessDataResult<Faculty>(result, "Faculty get Successfully");
        }

        public async Task<IResult> Update(Faculty entity)
        {
            await _facultyDal.Update(entity);
            return new SuccessResult("Faculty Updated Successfully");
        }
    }
}
