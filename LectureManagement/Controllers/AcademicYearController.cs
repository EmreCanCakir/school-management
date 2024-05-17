using LectureManagement.Model;
using LectureManagement.Model.Dtos;
using LectureManagement.Services.Abstracts;
using Microsoft.AspNetCore.Mvc;

namespace LectureManagement.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AcademicYearController : ControllerBase
    {
        private readonly IAcademicYearService _academicYearService;

        public AcademicYearController(IAcademicYearService academicYearDal)
        {
            _academicYearService = academicYearDal;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _academicYearService.GetAll();
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetAcademicYearById(Guid id)
        {
            var result = _academicYearService.GetById(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddAcademicYear([FromBody] AcademicYearDto academicYear)
        { 
            var result = await _academicYearService.Add(academicYear);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateAcademicYear([FromBody] AcademicYearUpdateDto academicYear)
        {
            var result = await _academicYearService.Update(academicYear);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(_academicYearService.Update(academicYear));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _academicYearService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("UpdateStatus/{id}")]
        public async Task<IActionResult> UpdateStatus(Guid id, AcademicYearStatus status)
        {
            var result = await _academicYearService.SetStatus(id, status);
            if (!result.Success)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }
    }
}
