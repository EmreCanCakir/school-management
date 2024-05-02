using AutoMapper;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Core
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Book, BookDto>()
                .ForMember(dest => dest.AuthorName, opt => opt.MapFrom(src => src.Author))
                .ReverseMap();
        }
    }
}
