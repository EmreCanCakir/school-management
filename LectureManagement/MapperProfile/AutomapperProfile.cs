using AutoMapper;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.MapperProfile
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Lecture, LectureAddDto>().ReverseMap();
            CreateMap<Lecture, LectureUpdateDto>().ReverseMap();
            CreateMap<Lecture, PrerequisiteDto>().ReverseMap();
            
            CreateMap<AcademicYear, AcademicYearDto>().ReverseMap();
            CreateMap<AcademicYear, AcademicYearUpdateDto>().ReverseMap();

            CreateMap<LectureSchedule, LectureScheduleAddDto>().ReverseMap();
            CreateMap<LectureSchedule, LectureScheduleUpdateDto>().ReverseMap();

            CreateMap<LectureInstructor, LectureInstructorAddDto>().ReverseMap();
            CreateMap<LectureInstructor, LectureInstructorUpdateDto>().ReverseMap();

            CreateMap<LectureStudent, LectureStudentAddDto>().ReverseMap();
            CreateMap<LectureStudent, LectureStudentUpdateDto>().ReverseMap();
        }
    }
}
