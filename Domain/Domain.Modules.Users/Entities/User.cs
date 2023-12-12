using Domain.Contracts;
using Domain.Domain.Modules.Users.Entities;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Entities.Enums;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User : AuditableEntity
    {
        public string PinCode { get; set; }
        public string? BusinessName { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public bool EmailConfirmed { get; set; } = default;
        public EmployeeType? EmployeeType { get; set; }
        public string? MobileNumber { get; set;}
        public bool PincodeVerified { get; set; } = default;
        public string Email { get; set; }
        public string? Department { get; set; }
        public string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public ICollection<UserRole> UserRoles { get; set; } = new HashSet<UserRole>();


        public void ChangePassword(string passwordHasH)
        {
            if (string.IsNullOrEmpty(passwordHasH))
                throw new ArgumentNullException(nameof(passwordHasH));

            PasswordHash = passwordHasH;
            
        }

        public void ChangePincode(string pincode)
        {
            if (string.IsNullOrEmpty(pincode))
                throw new ArgumentNullException(nameof(pincode));


            PinCode = pincode;
           
        }



    }
}
