using System.ComponentModel.DataAnnotations;
using LectureManagement.Core.Entities;

namespace LectureManagement.Model
{
    public class Book: IEntity
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int Year { get; set; }
    }
}
