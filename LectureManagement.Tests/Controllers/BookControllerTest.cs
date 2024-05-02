using FluentAssertions;
using Moq;
using LectureManagement.Services;
using LectureManagement.Controllers;
using LectureManagement.Model;
using LectureManagement.Model.Dtos;

namespace LectureManagement.Tests.Controllers
{
    public class BookControllerTest
    {
        [Fact]
        public void BooksController_ShouldBeAssignableToBaseController()
        {
            var mockBookService = new Mock<IBookService>();
            mockBookService.Setup(x => x.GetAll());

            var underTest = new BooksController(mockBookService.Object);

            underTest.Should().BeAssignableTo<BaseController<BookDto, IBookService>>();
        }
    }
}