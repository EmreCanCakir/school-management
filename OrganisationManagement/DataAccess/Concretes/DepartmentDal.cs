using Infrastructure.DataAccess.EntityFramework;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Concretes
{
    public class DepartmentDal: EfEntityRepositoryBase<Department, MainDbContext>, IDepartmentDal
    {
        public DepartmentDal(MainDbContext dbContext) : base(dbContext)
        {
        }
    }
}
