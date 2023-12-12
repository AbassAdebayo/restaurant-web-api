using Domain.Contracts;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Domain.Modules.Users.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role 
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public string RoleName { get; set; }
        public string Description { get; set; }
        public string CreatedBy { get; set; }

        //[JsonIgnore]
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();

        //[JsonIgnore]
        public ICollection<RolePermission> RolePermissions { get; set; } = new HashSet<RolePermission>();
    }
}
