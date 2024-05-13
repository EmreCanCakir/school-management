using Microsoft.AspNetCore.Mvc;
using OrganisationManagement.Model.Dtos;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;

namespace OrganisationManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FacultyController : ControllerBase
    {
        private readonly IFacultyService _facultyService;
        private readonly ILogger<FacultyController> _logger;

        public FacultyController(ILogger<FacultyController> logger, IFacultyService facultyService)
        {
            _logger = logger;
            _facultyService = facultyService;
        }

        [HttpGet]
        public IActionResult GetFaculties()
        {
            var faculties = _facultyService.GetAll();
            if (!faculties.Success)
            {
                return BadRequest(faculties.Message);
            }

            return Ok(faculties);
        }

        [HttpGet("{id}")]
        public IActionResult GetFacultyById(Guid id)
        {
            var faculty = _facultyService.GetById(id);
            if (!faculty.Success)
            {
                return BadRequest(faculty.Message);
            }

            return Ok(faculty);
        }

        [HttpPost]
        public async Task<IActionResult> AddFaculty([FromBody] FacultyAddDto faculty)
        {
            IResult result = await _facultyService.Add(faculty);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFaculty([FromBody] FacultyUpdateDto faculty)
        {
            IResult result = await _facultyService.Update(faculty);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFaculty(Guid id)
        {
            IResult result = await _facultyService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
