using Infrastructure.Utilities.Results;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Services.Concretes
{
    public class LectureService : ILectureService
    {
        private readonly ILectureDal _lectureDal;

        public LectureService(ILectureDal lectureDal)
        {
            _lectureDal = lectureDal;
        }

        public async Task<IResult> Add(Lecture entity)
        {
            await _lectureDal.Add(entity);
            return new SuccessResult("Lecture Created Successfully");
        }

        public async Task<IResult> Delete(Lecture entity)
        {
            await _lectureDal.Delete(entity);
            return new SuccessResult("Lecture Deleted Successfully");
        }

        public IDataResult<List<Lecture>> GetAll()
        {
            var result = _lectureDal.GetAll();
            return new SuccessDataResult<List<Lecture>>(result, "Lectures Listed Successfully");
        }

        public IDataResult<Lecture> GetById(Guid id)
        {
            var result = _lectureDal.Get(x => x.Id == id);
            return new SuccessDataResult<Lecture>(result, "Lecture Listed Successfully");
        }

        public async Task<IResult> Update(Lecture entity)
        {
            await _lectureDal.Update(entity);
            return new SuccessResult("Lecture Updated Successfully");
        }
    }
}
