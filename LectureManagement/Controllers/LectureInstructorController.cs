using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LectureInstructorController: ControllerBase
    {
        private readonly ILectureInstructorService _lectureInstructorService;

        public LectureInstructorController(ILectureInstructorService lectureInstructorService)
        {
            _lectureInstructorService = lectureInstructorService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAllLectures()
        {
            var lectures = _lectureInstructorService.GetAll();
            if (!lectures.Success)
            {
                return BadRequest(lectures.Message);
            }

            return Ok(lectures);
        }

        [HttpGet("{id}")]
        public IActionResult GetLectureInstructorById(Guid id)
        {
            var lecture = _lectureInstructorService.GetById(id);
            if (!lecture.Success)
            {
                return BadRequest(lecture.Message);
            }

            return Ok(lecture);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddLectureInstructor([FromBody] LectureInstructorAddDto lecture)
        {
            var result = await _lectureInstructorService.Add(lecture);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateLectureInstructor([FromBody] LectureInstructorUpdateDto lecture)
        {
            var result = await _lectureInstructorService.Update(lecture);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLectureInstructor(Guid id)
        {
            var result = await _lectureInstructorService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
