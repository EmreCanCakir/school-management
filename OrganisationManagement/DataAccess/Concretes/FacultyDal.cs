using Infrastructure.DataAccess.EntityFramework;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Concretes
{
    public class FacultyDal : EfEntityRepositoryBase<Faculty, MainDbContext>, IFacultyDal
    {
    }
}
