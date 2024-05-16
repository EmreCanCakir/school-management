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
    public class ClassroomService : IClassroomService
    {
        private readonly IClassroomDal _classroomDal;
        private readonly IMapper _mapper;

        public ClassroomService(IClassroomDal classroomDal, IMapper mapper)
        {
            _classroomDal = classroomDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(ClassroomAddDto entity)
        {
            var classroom = _mapper.Map<Classroom>(entity);
            await _classroomDal.Add(classroom);
            return new SuccessResult("Classroom Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var entity = _classroomDal.Get(x => x.Id == id);
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

        public async Task<IResult> Update(ClassroomUpdateDto entity)
        {
            var classroom = _mapper.Map<Classroom>(entity);
            await _classroomDal.Update(classroom, classroom.Id);
            return new SuccessResult("Classroom Updated Successfully");
        }
    }
}
