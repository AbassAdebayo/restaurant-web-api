using Application.DTOs;
using Domain.Domain.Modules.Users.Entities.Enums;
using Domain.Entities;
using Domain.Entities.Enums;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Email { get; set; }

        public IEnumerable<Role> Roles { get; set; } = new List<Role>();
    }

    public class UsersInRoleResponseModel : BaseResponse
    {
        public Guid Id { get; set; }

        public string Email { get; set; }
     
        //public string RoleName { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
    }

    public class SuperAdminUserResponseModel : BaseResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string BusinessName { get; set; }
        public string BusinessEMail { get; set; }
        public UserType UserType { get; set; }
        public IEnumerable<RoleDto> Roles { get; set; } = new List<RoleDto>();
        public string RoleName { get; set; }
        public bool EmailConfirmed { get; set; }
    }


    public class UsersResponseModel : BaseResponse
    {
        public PaginatedList<UserDto> Data { get; set; }
    }

    public class LoginRequestModel
    {
        [Required]
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class LoginResponseModel : BaseResponse
    {
        public LoginResponseData Data { get; set; }
    }

    public class LoginResponseData
    {
       public string Email { get; set; }
       public IEnumerable<string> Roles { get; set; } = new List<string>();
       public string Token { get; set; }
       public Guid UserId {get; set;}
       public string BusinessName {get; set;}}

    }



public class TokenResponseData
    {
        public string Email { get; set; }
        public string RoleName { get; set; }
        public Guid Id { get; set; }
        public string BusinessName { get; set; }
        public string BusinessEmail { get; set; }
        public UserType UserType { get; set; }
        public string Token { get; set; }
    }

    public class TokenRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class VerifyUserResponse : BaseResponse
    {
        public string Email { get; set; }
        public string BusinessName { get; set; }
        public string FirstName { get; set; }
        public EmployeeType EmployeeType { get; set; }
       //public IEnumerable<string> Roles { get; set; } = new List<string>();
        public Guid UserId { get; set; }
        

    }   

