using Application.DTOs;
using Application.Implementations.Modules.RolePermission.Services;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.RoleAndPermissionModule
{
    [Route("api/subpermission")]
    [ApiController]
    public class SubPermissionController : ControllerBase
    {
        private readonly ISubPermissionService _subPermissionService;

        public SubPermissionController(ISubPermissionService subPermissionService)
        {
            _subPermissionService = subPermissionService;
        }

        [HttpGet("get-subpermission/{subPermissionId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetSubPermissionById([FromRoute] Guid subPermissionId)
        {
            var response = await _subPermissionService.GetSubPermissionByIdAsync(subPermissionId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-subpermission")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllSubPermissions()
        {
            var response = await _subPermissionService.GetSubPermissionsAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
