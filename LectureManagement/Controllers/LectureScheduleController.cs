using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureScheduleController: ControllerBase
    {
        private readonly ILectureScheduleService _lectureScheduleService;

        public LectureScheduleController(ILectureScheduleService lectureScheduleService)
        {
            _lectureScheduleService = lectureScheduleService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _lectureScheduleService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetLectureStudent(Guid id)
        {
            var result = _lectureScheduleService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLectureSchedule([FromBody] LectureScheduleAddDto lectureSchedule)
        {
            var result = await _lectureScheduleService.Add(lectureSchedule);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLectureSchedule([FromBody] LectureScheduleUpdateDto lectureSchedule)
        {
            var result = await _lectureScheduleService.Update(lectureSchedule);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _lectureScheduleService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
