using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using LectureManagement.Controllers;
using LectureManagement.Model.Dtos;
using LectureManagement.Services;

namespace LectureManagement.Tests.Controllers
{
    public class BaseControllerTest
    {
        #region GetAll
        [Fact]
        public async Task GetAll_OnSuccess_ShouldReturns200()
        {
            // Arrange
            var mockService = new Mock<IBookService>();
            mockService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<BookDto>()
                {
                    new BookDto { Title = "Book1", AuthorName = "Author1", Year = 2020 },
                }));

            var underTest = new BaseController<BookDto, IBookService>(mockService.Object);

            // Act
            var result = (OkObjectResult)await underTest.GetAll();

            // Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAll_OnSuccess_InvokeServiceTimesOnce()
        {
            var mockService = new Mock<IBookService>();
            mockService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<BookDto>()
                {
                    new BookDto { Title = "Book1", AuthorName = "Author1", Year = 2020 },
                }));

            var underTest = new BaseController<BookDto, IBookService>(mockService.Object);

            var result = await underTest.GetAll();

            mockService.Verify(x => x.GetAll(), Times.Once);
        }

        [Fact]
        public async Task GetAll_OnSuccess_ShouldReturnListOfEntity()
        {
            var mockService = new Mock<IBookService>();
            mockService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<BookDto>()
                {
                    new BookDto { Title = "Book1", AuthorName = "Author1", Year = 2020 },
                }));

            var underTest = new BaseController<BookDto, IBookService>(mockService.Object);
            var result = await underTest.GetAll();
            var objectResult = (OkObjectResult)result;

            result.Should().BeOfType<OkObjectResult>();
            objectResult.Value.Should().BeOfType<List<BookDto>>();
        }

        [Fact]
        public async Task GetAll_OnNoEntitiesFound_ShouldReturn500()
        {
            var mockService = new Mock<IBookService>();
            mockService.Setup(x => x.GetAll())
                .Returns(Task.FromResult(new List<BookDto>()));

            var underTest = new BaseController<BookDto, IBookService>(mockService.Object);
            var result = await underTest.GetAll();
            var objectResult = (BadRequestResult)result;

            result.Should().BeOfType<BadRequestResult>();
            objectResult.StatusCode.Should().Be(400);
        }
        #endregion

    }
}
