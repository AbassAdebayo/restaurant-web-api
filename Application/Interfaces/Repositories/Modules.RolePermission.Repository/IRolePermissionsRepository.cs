using Domain.Domain.Modules.RolePermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.RolePermission.Repository
{
    public interface IRolePermissionsRepository
    {
        Task<Domain.Domain.Modules.RolePermission.Entities.RolePermission> AddRolePermissionAsync(Domain.Domain.Modules.RolePermission.Entities.RolePermission rolePermission);
        Task<IEnumerable<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> AddRolePermissionsAsync(IEnumerable<Domain.Domain.Modules.RolePermission.Entities.RolePermission> rolePermissions);
        Task<Domain.Domain.Modules.RolePermission.Entities.RolePermission> GetRolePermissionByIdAsync(Guid rolePermissionId);
        Task<IList<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> GetAllRolesWithPermissionsAndSubPermissionsAsync();
        Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId);
        Task<List<Permission>> GetPermissionsByRoleAsync(Guid roleId);
        Task<bool> UserHasPermissionAsync(Guid userId, string permissionName);
        Task<bool> UserHasPermissionAndSubPermissionAsync(Guid userId, List<string> permissionNames, List<string> subPermissionNames);



    }
}
