using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class RoleDto
    {
        public Guid RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        public List<PermissionDto> Permissions { get; set; }
        public List<SubPermissionDto> SubPermissions { get; set; }

        //[JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        //[JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }


    public class PermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        
    }

    public class SubPermissionDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        // Include other SubPermission properties you want to expose
    }

    public class GroupedPermissionsDto
    {
        public Dictionary<string, PermissionDto> Permissions { get; set; }
    }

    public class RolesResponseModel : BaseResponse<Role>
    {
        public IList<Role> Data { get; set; }
    }

    public class AdminRoleResponseModel : BaseResponse<Role>
    {
        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
    }

    public class CreateRoleRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Guid> SelectedPermissionIds { get; set; } 
        public List<Guid> SelectedSubPermissionIds { get; set; }
    }

    public class UpdateRoleRequestModel
    {
        public string Name { get; set; } // Updated role name
        public string Description { get; set; } // Updated role description
        public List<Guid> SelectedPermissionIds { get; set; } // IDs of selected permissions
        public List<Guid> SelectedSubPermissionIds { get; set; } // IDs of se
    }

}
