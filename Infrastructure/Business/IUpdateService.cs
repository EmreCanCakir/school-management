using Infrastructure.Utilities.Results;
namespace Infrastructure.Business
{
    public interface IUpdateService<TDto> where TDto : class, new()
    {
        Task<IResult> Update(TDto entity);
    }
}
