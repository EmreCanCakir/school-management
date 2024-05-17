using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureStudentController: ControllerBase
    {
        private readonly ILectureStudentService _lectureStudentService;

        public LectureStudentController(ILectureStudentService lectureStudentService)
        {
            _lectureStudentService = lectureStudentService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _lectureStudentService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetLectureStudent(Guid id)
        {
            var result = _lectureStudentService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLectureStudent([FromBody] LectureStudentAddDto lectureStudent)
        {
            var result = await _lectureStudentService.Add(lectureStudent);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLectureStudent([FromBody] LectureStudentUpdateDto lectureStudent)
        {
            var result = await _lectureStudentService.Update(lectureStudent);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _lectureStudentService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
