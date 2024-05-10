using Infrastructure.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Business
{
    public interface IBaseService<T,I>
    {
        Task<IResult> Add(T entity);
        Task<IResult> Delete(T entity);
        Task<IResult> Update(T entity);
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(I id);
    }
}
