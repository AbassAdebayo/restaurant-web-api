using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Domain.Contracts;
using MassTransit;

namespace Domain.Domain.Modules.RolePermission.Entities
{
    public class SubPermission
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public string Name { get; set; }
        public Guid PermissionId { get; set; }

        //[JsonIgnore]
        public Permission? Permission { get; set; }
    }
}
