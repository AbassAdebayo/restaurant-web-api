using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.RolesAndPermissions.Service
{
    public interface IRoleService
    {
        public Task<BaseResponse<Role>> GetRoleAsync(string name);
        public Task<BaseResponse<Role>> GetRoleByIdAsync(Guid roleId);
        public Task<BaseResponse<bool>> RoleExistsAsync(string name, string businessName);
        public Task<BaseResponse<IList<RoleDto>>> GetRolesBySuperAdminAsync(string businessName);
        public Task<BaseResponse<bool>> DeleteRole(Guid roleId);
        
    }
}
