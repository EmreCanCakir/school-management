using Infrastructure.DataAccess.EntityFramework;
using OrganisationManagement.DataAccess.Abstracts;
using OrganisationManagement.Model;

namespace OrganisationManagement.DataAccess.Concretes
{
    public class LectureDal: EfEntityRepositoryBase<Lecture, MainDbContext>, ILectureDal
    {
    }
}
