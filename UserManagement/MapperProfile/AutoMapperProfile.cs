using AutoMapper;
using UserManagement.Models;
using UserManagement.Models.Dtos;

namespace UserManagement.MapperProfile
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UserRegisterDto, UserDetail>()
                .Include<StudentRegisterDto, Student>()
                .Include<LecturerRegisterDto, Lecturer>()
                .ReverseMap();

            CreateMap<StudentRegisterDto, Student>()
                .IncludeBase<UserRegisterDto, UserDetail>()
                .ReverseMap();

            CreateMap<LecturerRegisterDto, Lecturer>()
                .IncludeBase<UserRegisterDto, UserDetail>()
                .ReverseMap();
        }
    }
}
