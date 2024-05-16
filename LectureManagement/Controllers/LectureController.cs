using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureController : ControllerBase
    {
        private readonly ILectureService _lectureService;

        public LectureController(ILectureService lectureService)
        {
            _lectureService = lectureService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllLectures()
        {
            var lectures = _lectureService.GetAll();
            if (!lectures.Success)
            {
                return BadRequest(lectures);
            }

            return Ok(lectures);
        }

        [HttpGet("{id}")]
        public IActionResult GetLectureById(Guid id)
        {
            var lecture = _lectureService.GetById(id);
            if (!lecture.Success)
            {
                return BadRequest(lecture);
            }

            return Ok(lecture);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLecture([FromBody] LectureAddDto lecture)
        {
            var result = await _lectureService.Add(lecture);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLecture([FromBody] LectureUpdateDto lecture)
        {
            var result = await _lectureService.Update(lecture);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLecture(Guid id)
        {
            var result = await _lectureService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
