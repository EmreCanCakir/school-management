using AutoMapper;
using Newtonsoft.Json;
using System.Text.Json;
using LectureManagement.Core.Cache;
using LectureManagement.DataAccess;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Services
{
    public class BookService: IBookService
    {
        private readonly IBookDal _bookDal;
        private readonly IMapper _autoMapper;
        private readonly ICacheService _cacheService;
        public BookService(IBookDal bookDal, IMapper autoMapper, ICacheService cacheService)
        {
            _bookDal = bookDal;
            _autoMapper = autoMapper;
            _cacheService = cacheService;
        }

        public async Task<List<BookDto>> GetAll()
        {
            List<Book> books = await _cacheService.GetAsync<List<Book>>("books");
            if(books is not null)
            {
                return _autoMapper.Map<List<BookDto>>(books);
            }

            books = _bookDal.GetAll();
            await _cacheService.SetAsync("books", books);
            return _autoMapper.Map<List<BookDto>>(books);
        }
    }
}
