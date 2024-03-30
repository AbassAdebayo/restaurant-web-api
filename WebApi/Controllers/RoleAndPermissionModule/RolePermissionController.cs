using Application.DTOs;
using Application.DTOs.RequestModel;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.RoleAndPermissionModule
{
    [Route("api/role-permission")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {
        private readonly ILogger<RolePermissionController> _logger;
        private readonly IRolePermissionService _rolePermissionService;
        private readonly IRoleService _roleService;

        public RolePermissionController(ILogger<RolePermissionController> logger, IRolePermissionService rolePermissionService, IRoleService roleService)
        {
            _logger = logger;
            _rolePermissionService = rolePermissionService;
            _roleService = roleService;
        }

        [HttpPost("create-role/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> Create([FromRoute] string token, [FromBody] CreateRoleRequestModel model)
        {
            var response = await _rolePermissionService.AddRoleAsync(token, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("clone-role/{roleId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> CloneRole([FromRoute] Guid roleId)
        {
            var response = await _rolePermissionService.CloneRoleAsync(roleId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("userhaspermission/{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> UserHasPermission([FromRoute] Guid userId, [FromQuery] string permissionName)
        {
            var response = await _rolePermissionService.UserHasPermissionAsync(userId, permissionName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("user-has-permission-and-subpermission/{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> UserHasPermissionAndSubPermission([FromRoute] Guid userId, [FromQuery] List<string> permissionNames, [FromQuery] List<string> subPermissionNames)
        {
            var response = await _rolePermissionService.UserHasPermissionAndSubPermissionAsync(userId, permissionNames, subPermissionNames);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("remove-permission-from-role/{roleId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> RemovePermissionFromRole([FromRoute] Guid roleId, [FromQuery] Guid permissionId)
        {
            var response = await _rolePermissionService.RemovePermissionFromRoleAsync(roleId, permissionId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-permissions-by-role")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetPermissionsByRole([FromQuery] Guid roleId)
        {
            var response = await _rolePermissionService.GetPermissionsByRoleAsync(roleId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-roles-with-permissions")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllRolePermissions()
        {
            var response = await _rolePermissionService.GetAllRolesWithPermissionsAsync();
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("role-exists")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> RoleExists([FromQuery] string roleName, [FromQuery] string businessName)
        {
            var response = await _roleService.RoleExistsAsync(roleName, businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-role-roles-by-super-admin")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllRoleBySuperAdmin([FromQuery] string businessName)
        {
            var response = await _roleService.GetRolesBySuperAdminAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete-role/{roleId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> DeleteRole([FromRoute] Guid roleId)
        {
            var response = await _roleService.DeleteRole(roleId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("edit-role/{roleId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EditRole([FromRoute] Guid roleId, [FromBody] UpdateRoleRequestModel model)
        {
            var response = await _rolePermissionService.UpdateRoleAsync(roleId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
