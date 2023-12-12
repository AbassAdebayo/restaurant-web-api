using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestModel.User
{
    public class CreateUserAdmin
    {
        public string BusinessName { get; set; }
        public string BusinessEmail { get; set; }
        public string PasswordHash { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string ConfirmPincode { get; set; }
    }

    public class CreateEmployee
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public EmployeeType EmployeeType { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public string Department { get; set; }
        public IEnumerable<string> RoleNames { get; set; }

    }
}
