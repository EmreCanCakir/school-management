using Infrastructure.DataAccess;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Abstracts
{
    public interface IDepartmentDal: IEntityRepository<Department>
    {
    }
}
