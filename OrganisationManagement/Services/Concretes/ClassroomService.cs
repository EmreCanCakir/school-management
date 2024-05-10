using Infrastructure.Business;
using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomDal _classroomDal;

        public ClassroomService(IClassroomDal classroomDal)
        {
            _classroomDal = classroomDal;
        }

        public async Task<IResult> Add(Classroom entity)
        {
            await _classroomDal.Add(entity);
            return new SuccessResult("Classroom Created Successfully");
        }

        public async Task<IResult> Delete(Classroom entity)
        {
            await _classroomDal.Delete(entity);
            return new SuccessResult("Classroom Deleted Successfully");
        }

        public IDataResult<List<Classroom>> GetAll()
        {
            var result = _classroomDal.GetAll();
            return new SuccessDataResult<List<Classroom>>(result, "Classrooms Listed Successfully");
        }

        public IDataResult<Classroom> GetById(Guid id)
        {
            var result = _classroomDal.Get(x => x.Id == id);
            return new SuccessDataResult<Classroom>(result, "Classroom Get Successfully");
        }

        public async Task<IResult> Update(Classroom entity)
        {
            await _classroomDal.Update(entity);
            return new SuccessResult("Classroom Updated Successfully");
        }
    }
}
