using EntityFrameworkCore.Testing.Moq;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LectureManagement.DataAccess;
using LectureManagement.Model;

namespace LectureManagement.Tests.DataAccess
{
    public class MainDbContextTest
    {
        [Fact]
        public void DbContext_ShouldWithDbContextOptions_IsAvailable()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            mockedDbContext.Should().NotBeNull();
        }

        [Fact]
        public void DbContext_DbSets_MustHaveDbSetWithTypeBookEntity()
        {
            var mockedDbContext = Create.MockedDbContextFor<MainDbContext>();
            Assert.True(mockedDbContext.Books is DbSet<Book>);
        }
    }
}
