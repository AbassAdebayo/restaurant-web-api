using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.RolePermission.Repository
{
    public interface IRoleRepository
    {
        Task<Role> AddRoleAsync(Role role);
        Task<bool> DeleteRole(Role role);
        Task<Role> GetRoleByIdAsync(Guid roleId);
        Task<IList<Role>> GetAllRolesBySuperAdminAsync(string businessName);
        Task<bool> RoleExistsBySuperAdmin(string roleName, string businessName);
        Task<Role> GetRoleByName(string roleName);
        Task<Role> UpdateRoleAsync(Role role);
        Task AssignRoleToUserAsync(Guid userId, Guid roleId);
        Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);
        Task<List<Role>> GetRolesByUserAsync(Guid userId);
    }
}
