using Microsoft.AspNetCore.Mvc;
using OrganisationManagement.Model.Dtos;
using OrganisationManagement.Services.Abstracts;
using IResult = Infrastructure.Utilities.Results.IResult;
namespace OrganisationManagement.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DepartmentController: ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        [HttpGet("GetAll")]
        public IActionResult GetDepartments()
        {
            var departments = _departmentService.GetAll();
            if (!departments.Success)
            {
                return BadRequest(departments.Message);
            }

            return Ok(departments);
        }

        [HttpGet("{id}")]
        public IActionResult GetDepartmentById(Guid id)
        {
            var department = _departmentService.GetById(id);
            if (!department.Success)
            {
                return BadRequest(department.Message);
            }

            return Ok(department);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddDepartment([FromBody] DepartmentAddDto department)
        {
            IResult result = await _departmentService.Add(department);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpPost("Update")]
        public async Task<IActionResult> UpdateDepartment([FromBody] DepartmentUpdateDto department)
        {
            IResult result = await _departmentService.Update(department);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            IResult result = await _departmentService.Delete(id);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result);
        }
    }
}
