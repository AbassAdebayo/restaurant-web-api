using Domain.Domain.Modules.Users.Entities;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class EmployeeDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public bool EmailConfirmed { get; set; }
        public bool PincodeVerified { get; set; } = default;
        public UserType UserType { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid EmployeeUserId { get; set; }
        public List<RoleDto> Roles { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();
    }

    public class UpdateEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
