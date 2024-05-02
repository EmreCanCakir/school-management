using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    public interface IController<T>
    {
        Task<IActionResult> GetAll();
    }
}
