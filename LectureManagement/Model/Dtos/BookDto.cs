using LectureManagement.Core.Entities;

namespace LectureManagement.Model.Dtos
{
    public class BookDto: IDto
    {
        public string Title { get; set; }
        public string AuthorName { get; set; }
        public int Year { get; set; }
    }
}
