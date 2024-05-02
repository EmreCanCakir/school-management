using Microsoft.AspNetCore.Mvc;
using LectureManagement.Core.Entities;

namespace LectureManagement.Services
{
    public interface IBaseService<T> where T : class, IEntity, new()
    {
        Task<List<T>> GetAll();
    }
}
