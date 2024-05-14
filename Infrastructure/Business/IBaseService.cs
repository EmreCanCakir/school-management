using Infrastructure.Utilities.Results;

namespace Infrastructure.Business
{
    public interface IBaseService<T,I>
    {
        Task<IResult> Delete(I id);
        IDataResult<List<T>> GetAll();
        IDataResult<T> GetById(I id);
    }
}
