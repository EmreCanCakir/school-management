using AutoMapper;
using Infrastructure.Utilities.Results;
using LectureManagement.DataAccess.Abstracts;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using System.Linq;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace LectureManagement.Services.Concretes
{
    public class LectureService : ILectureService
    {
        private readonly ILectureDal _lectureDal;
        private readonly IMapper _mapper;
        public LectureService(ILectureDal lectureDal, IMapper mapper)
        {
            _lectureDal = lectureDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(LectureAddDto entity)
        {
            var lecture = _mapper.Map<Lecture>(entity);
            if (lecture == null)
            {
                return new ErrorResult("An error occurred while mapping the lecture schedule");
            }

            var isUnique = IsLectureGroupAndCodeUnique(lecture);
            if (!isUnique.Success)
            {
                return isUnique;
            }

            if (lecture.Prerequisites.Any())
            {
                List<Lecture> prerequisities = new List<Lecture>();
                foreach (var prerequisite in lecture.Prerequisites)
                {
                    var foundPrerequisite = _lectureDal.Get(x => x.Id == prerequisite.Id);
                    if (foundPrerequisite != null)
                    {
                        prerequisities.Add(foundPrerequisite);
                    }
                }

                if(prerequisities.Any())
                {
                    lecture.Prerequisites = prerequisities;
                }
            }

            await _lectureDal.Add(lecture);
            return new SuccessResult("Lecture Created Successfully");
        }

        public async Task<IResult> Delete(Guid id)
        {
            var entity = _lectureDal.Get(x => x.Id == id);
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

        public async Task<IResult> Update(LectureUpdateDto entity)
        {
            var lecture = _mapper.Map<Lecture>(entity);
            if(lecture == null)
            {
                return new ErrorResult("Lecture Not Found");
            }

            var isUnique = IsLectureGroupAndCodeUnique(lecture);
            if (lecture.Code != null && !isUnique.Success)
            {
                return isUnique;
            }

            await _lectureDal.Update(lecture, lecture.Id);
            return new SuccessResult("Lecture Updated Successfully");
        }

        private IResult IsLectureGroupAndCodeUnique(Lecture entity)
        {
            var result = _lectureDal.Get(x => x.Code == entity.Code);
            if (result != null && (result.IsGroup == false || entity.IsGroup == false) && result.Id != entity.Id)
            {
                return new ErrorResult("Lecture Code Must Be Unique");
            }

            return new SuccessResult();
        }
    }
}
