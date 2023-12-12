using Application.DTOs;
using Domain.Domain.Modules.RolePermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.RolesAndPermissions.Service
{
    public interface IPermissionService
    {
        public Task<BaseResponse<Permission>> GetPermissionByIdAsync(Guid permissionId);
        public Task<BaseResponse<Permission>> GetPermissionByNameAsync(string permissionName);
        public Task<BaseResponse<IList<BaseResponse<Permission>>>> GetPermissionsByIdsAsync(List<Guid> permissionIds);
        // public Task<BaseResponse<IList<BaseResponse<Permission>>>> GetAllPermissionsAsync();
        public Task<BaseResponse<Dictionary<string, Dictionary<string, object>>>> GetAllPermissionsAsync();

    }
}
