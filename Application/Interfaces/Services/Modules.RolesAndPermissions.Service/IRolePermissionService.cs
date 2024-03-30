using Application.DTOs;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.RolesAndPermissions.Service
{
    public interface IRolePermissionService
    {
        public Task<BaseResponse<Role>> AddRoleAsync(string superAdminUserToken, CreateRoleRequestModel model);
        Task<BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> GetRolePermissionByIdAsync(Guid rolePermissionId);
        Task<BaseResponse<IList<RoleDto>>> GetAllRolesWithPermissionsAsync();
        Task<BaseResponse<bool>> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
        public Task<BaseResponse<IList<BaseResponse<Permission>>>> GetPermissionsByRoleAsync(Guid roleId);
        Task<BaseResponse<bool>> UserHasPermissionAsync(Guid userId, string permissionName);
        Task<BaseResponse<bool>> UserHasPermissionAndSubPermissionAsync(Guid userId, List<string> permissionNames, List<string> subPermissionNames);
        Task<BaseResponse<Role>> CloneRoleAsync(Guid roleId);
        public Task<BaseResponse<Role>> UpdateRoleAsync(Guid roleId, UpdateRoleRequestModel model);

    }
}
