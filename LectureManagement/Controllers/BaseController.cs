using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using LectureManagement.Core.Entities;
using LectureManagement.Services;

namespace LectureManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BaseController<TEntity, TService> : ControllerBase, IController<TEntity>
        where TEntity : class, IEntity, new()
        where TService : class, IBaseService<TEntity>
    {
        private readonly TService _service;
        public BaseController(TService service)
        {
            _service = service;
        }

        [HttpGet(Name = "GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var entities = await _service.GetAll();
            if (!entities.Any())
            {
                return BadRequest();
            }
            return Ok(entities);
        }
    }
}
