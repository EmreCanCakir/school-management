using Infrastructure.Utilities.Results;

namespace Infrastructure.Business
{
    public interface IAddService<TDto> where TDto : class, new()
    {
        Task<IResult> Add(TDto entity);
    }
}
