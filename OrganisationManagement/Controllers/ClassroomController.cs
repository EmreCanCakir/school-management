using Microsoft.AspNetCore.Mvc;
using OrganisationManagement.Model.Dtos;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace OrganisationManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ClassroomController: ControllerBase
    {
        private readonly IClassroomService _classroomService;

        public ClassroomController(IClassroomService classroomService)
        {
            _classroomService = classroomService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetClassrooms()
        {
            var classrooms = _classroomService.GetAll();
            if (!classrooms.Success)
            {
                return BadRequest(classrooms);
            }

            return Ok(classrooms);
        }

        [HttpGet("{id}")]
        public IActionResult GetClassroomById(Guid id)
        {
            var classroom = _classroomService.GetById(id);
            if (!classroom.Success)
            {
                return BadRequest(classroom);
            }

            return Ok(classroom);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddClassroom([FromBody] ClassroomAddDto classroom)
        {
            IResult result = await _classroomService.Add(classroom);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateClassroom([FromBody] ClassroomUpdateDto classroom)
        {
            IResult result = await _classroomService.Update(classroom);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClassroom(Guid id)
        {
            IResult result = await _classroomService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
