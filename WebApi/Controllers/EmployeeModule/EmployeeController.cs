using Application.DTOs;
using Application.DTOs.RequestModel.User;
using Application.Interfaces.Modules.Employee.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.EmployeeModule
{
    [Route("api/employee")]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpPost("invite-employee/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateEmployee))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> InviteEmployee([FromBody] CreateEmployee model, [FromRoute] string token)
        {

            var response = await _employeeService.CreateEmployee(token, model);
            return Ok(response);

        }

        [HttpGet("get-employee-by-id/{employeeUserId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetPermissionById([FromRoute] Guid employeeUserId)
        {
            var response = await _employeeService.GetEmployeeByIdAsync(employeeUserId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-employees-by-super-admin")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllEmployeesBySuperAdmin([FromQuery] string businessName)
        {
            var response = await _employeeService.GetAllEmployeesBySuperAdminAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("update-employee/{employeeUserId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid employeeUserId, [FromBody] UpdateEmployeeRequest request)
        {
            var response = await _employeeService.UpdateEmployeeAsync(employeeUserId, request);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete-employee/{employeeUserId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid employeeUserId)
        {
            var response = await _employeeService.DeleteEmployee(employeeUserId);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
