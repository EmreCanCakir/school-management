using EntityFrameworkCore.Testing.Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using LectureManagement.Core.DataAccess;
using LectureManagement.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.Tests.Core.DataAccess
{
    public class EfEntityRepositoryBaseTest
    {
        [Fact]
        public void EfEntityRepositoryBase_IsAssignableFromIEntityRepository()
        {
            var context = new MainDbContext();
            var underTest = new EfEntityRepositoryBase<Book, MainDbContext>(context);

            underTest.Should().BeAssignableTo<IEntityRepository<Book>>();
        }

        [Fact]
        public void GetAll_ShouldInvokeDbContextAndFilterResult()
        {
            var options = new DbContextOptionsBuilder<MainDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDB")
                .Options;

            using (var context = new MainDbContext(options))
            {
                var mockData = new List<Book>
                    {
                        new Book { Title = "Book1", Author = "Author1", Year = 2020 },
                        new Book { Title = "Book2", Author = "Author2", Year = 2020 }
                    };
                Expression<Func<Book, bool>> titleFilter = book => book.Title == "Book2";
                context.Books.AddRange(mockData);
                context.SaveChanges();

                var repository = new EfEntityRepositoryBase<Book, MainDbContext>(context);
                var result = repository.GetAll(titleFilter);

                result.Should().BeEquivalentTo([mockData.Last()]);
            }
        }
    }
}
