using EntityFrameworkCore.Testing.Moq;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureManagement.Core.DataAccess;
using LectureManagement.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.Tests.DataAccess
{
    public class EfBookDalTest
    {
        [Fact]
        public void EfBookDal_ShouldBeAssignableToEfEntityRepositoryBase()
        {
            var context = Create.MockedDbContextFor<MainDbContext>();
            var underTest = new EfBookDal(context);

            underTest.Should().BeAssignableTo<EfEntityRepositoryBase<Book, MainDbContext>>();
        }
    }
}
