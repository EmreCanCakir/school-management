using AutoMapper;
using Infrastructure.Business;
using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class FacultyService : IFacultyService
    {
        private readonly IFacultyDal _facultyDal;
        private readonly IMapper _mapper;

        public FacultyService(IFacultyDal facultyDal, IMapper mapper)
        {
            _facultyDal = facultyDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(FacultyAddDto entity)
        {
            var faculty = _mapper.Map<Faculty>(entity);
            await _facultyDal.Add(faculty);
            return new SuccessResult("Faculty Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var faculty = _facultyDal.Get(x => x.Id == id);
            await _facultyDal.Delete(faculty);
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

        public async Task<IResult> Update(FacultyUpdateDto entity)
        {
            var faculty = _mapper.Map<Faculty>(entity);
            await _facultyDal.Update(faculty, faculty.Id);
            return new SuccessResult("Faculty Updated Successfully");
        }
    }
}
