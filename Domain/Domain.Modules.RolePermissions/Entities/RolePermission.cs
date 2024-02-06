using Domain.Contracts;
using Domain.Entities;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Domain.Modules.RolePermission.Entities
{
    public class RolePermission
    {
        public Guid Id { get; set; } = NewId.Next().ToGuid();
        public Guid RoleId { get; set; }
        public Role Role { get; set; }
        public Guid PermissionId { get; set; }
        public Permission? Permission { get; set; }
        public Guid? SubPermissionId { get; set; }
        public SubPermission? SubPermission { get; set; }

    }
}
