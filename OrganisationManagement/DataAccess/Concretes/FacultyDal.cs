using Infrastructure.DataAccess.EntityFramework;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Concretes
{
    public class FacultyDal : EfEntityRepositoryBase<Faculty, MainDbContext>, IFacultyDal
    {
        public FacultyDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
