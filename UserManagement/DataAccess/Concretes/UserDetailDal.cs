using Infrastructure.DataAccess;
using Infrastructure.DataAccess.EntityFramework;
using Infrastructure.Entities.Abstracts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using UserManagement.DataAccess.Abstracts;
using UserManagement.Models;

namespace UserManagement.DataAccess.Concretes
{
    public class UserDetailDal : IUserDetailDal
    {
        private readonly MainDbContext _context;
        public UserDetailDal(MainDbContext context)
        {
            _context = context;
        }

        async Task IEntityRepository<UserDetail>.Add(UserDetail entity)
        {
            var addedEntity = _context.Entry(entity);
            addedEntity.State = EntityState.Added;
            await _context.SaveChangesAsync();
        }

        async Task IEntityRepository<UserDetail>.Delete(UserDetail entity)
        {
            var deletedEntity = _context.Entry(entity);
            deletedEntity.State = EntityState.Deleted;
            await _context.SaveChangesAsync();
        }

        UserDetail IEntityRepository<UserDetail>.Get(Expression<Func<UserDetail, bool>> filter)
        {
            return _context.Set<UserDetail>().FirstOrDefault(filter);
        }

        List<UserDetail> IEntityRepository<UserDetail>.GetAll(Expression<Func<UserDetail, bool>> filter)
        {
            return filter == null
                ? _context.Set<UserDetail>().ToList()
                : _context.Set<UserDetail>().Where(filter).ToList();
        }

        async Task IEntityRepository<UserDetail>.Update(UserDetail entity)
        {
            var updatedEntity = _context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
    }
}
