using Application.DTOs;
using Application.Implementations.Modules.RolePermission.Services;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.RoleAndPermissionModule
{
    [Route("api/permission")]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpGet("get-permission-by-id/{permissionId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetPermissionById([FromRoute] Guid permissionId)
        {
            var response = await _permissionService.GetPermissionByIdAsync(permissionId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-permission-by-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetPermissionByNme([FromBody] string permissionName)
        {
            var response = await _permissionService.GetPermissionByNameAsync(permissionName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-permissions")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllPermissions()
        {
            var response = await _permissionService.GetAllPermissionsAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }


        [HttpGet("get-permissions-by-ids")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllPermissionsByIds([FromQuery] List<Guid> permissionIds)
        {
            var response = await _permissionService.GetPermissionsByIdsAsync(permissionIds);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
