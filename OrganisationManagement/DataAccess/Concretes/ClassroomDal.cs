using Infrastructure.DataAccess.EntityFramework;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Concretes
{
    public class ClassroomDal : EfEntityRepositoryBase<Classroom, MainDbContext>, IClassroomDal
    {
        public ClassroomDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
