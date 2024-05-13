using AutoMapper;
using OrganisationManagement.Model;
using OrganisationManagement.Model.Dtos;

namespace OrganisationManagement.MapperProfile
{
    public class AutomapperProfile: Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Faculty, FacultyAddDto>().ReverseMap();
            CreateMap<Faculty, FacultyUpdateDto>().ReverseMap();
            CreateMap<Department, DepartmentAddDto>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDto>().ReverseMap();
            CreateMap<Classroom, ClassroomAddDto>().ReverseMap();
            CreateMap<Classroom, ClassroomUpdateDto>().ReverseMap();
        }
    }
}
