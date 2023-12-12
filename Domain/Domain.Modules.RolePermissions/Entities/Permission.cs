using Domain.Contracts;
using Domain.Domain.Modules.Users.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.RolePermission.Entities
{
    public class Permission
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public string Name { get; set; }

        //[JsonIgnore]
        public ICollection<SubPermission> SubPermissions { get; set; } = new HashSet<SubPermission>();

       
    }
}
