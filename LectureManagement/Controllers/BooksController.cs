using Microsoft.AspNetCore.Mvc;
using LectureManagement.Model.Dtos;
using LectureManagement.Services;

namespace LectureManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BooksController : BaseController<BookDto, IBookService>
    {
        private readonly IBookService _service;
        public BooksController(IBookService service) : base(service)
        {
            _service = service;
        }
    }
}
