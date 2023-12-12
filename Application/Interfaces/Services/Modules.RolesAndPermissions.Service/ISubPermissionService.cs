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
    public interface ISubPermissionService
    {
        public Task<BaseResponse<SubPermission>> GetSubPermissionByIdAsync(Guid subPermissionId);
        public Task<BaseResponse<IList<BaseResponse<SubPermission>>>> GetSubPermissionsAsync();
    }
}
