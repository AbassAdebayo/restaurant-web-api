using Application.DTOs;
using Application.DTOs.RequestModel.User;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Modules.Employee.Repositories;
using Application.Interfaces.Modules.Employee.Services;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Application.Implementations.Modules.Employee.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(IEmployeeRepository employeeRepository, UserManager<User> userManager, IIdentityService identityService, IUserRepository userRepository, IMailService mailService, ILogger<EmployeeService> logger)
        {
            _employeeRepository = employeeRepository;
            _userManager = userManager;
            _identityService = identityService;
            _userRepository = userRepository;
            _mailService = mailService;
            _logger = logger;
        }

        public async Task<BaseResponse> CreateEmployee(string userToken, CreateEmployee request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                throw new BadRequestException("User not authenticated");
            }

            //if(!user.EmailConfirmed) throw new BadRequestException("User unverified");

            //if (!user.PincodeVerified) return new BaseResponse { Message = "pincode not verified", Status = false };

            if (user.UserType != UserType.SuperAdmin) throw new BadRequestException("Unauthorized!");
            
            var userNameExist = await _userRepository.AnyAsync(u => u.Email == request.Email);
            if (userNameExist)
            {
                throw new BadRequestException("Employee Already Exist!");
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var employee = new User
            {
                Name = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                MobileNumber = request.MobileNumber,
                Department = request.Department,
                EmployeeType = request.EmployeeType,
                UserType = Domain.Entities.Enums.UserType.Employee,
                CreatedBy = user.BusinessName,
                CreatedOn = DateTime.UtcNow
            };
            employee.PasswordHash = _identityService.GetPasswordHash("1234");
            employee.PinCode = _identityService.GetPincodeHash("1234");
            var newUser = await _userManager.CreateAsync(employee);
            if (newUser == null)
            {
                throw new Exception("Employee Creation Unsuccessful");
            }
            var result = await _userManager.AddToRolesAsync(employee, request.RoleNames);

            if (!result.Succeeded)
            {
                throw new Exception($"Unable to add employee to the {request.RoleNames} role");
            }

            var roles = await _userManager.GetRolesAsync(employee);
            //Send verification mail
            var token = _identityService.GenerateToken(employee, roles);

            //Send verification mail
            //var sent = await _mailService.SendInvitationMail(employee.Email, employee.Name, token, roles);
            //if (!sent)
            //{
            //    _logger.LogWarning($"User created, but unable to Send an Invite to user with Email {employee.Email}");
            //    return new BaseResponse
            //    {
            //        Message = $"User created, but unable to Send an Invite to user with Email {employee.Email}",
            //        Status = false
            //    };
            //}

            _logger.LogWarning($"User created successfully");
            return new BaseResponse
            {
                Message = $"An Invitation mail has been sent to {employee.Email}",
                Status = true
            };

            
        }

        public async Task<BaseResponse<bool>> DeleteEmployee(Guid employeeUserId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeUserId);
            if (employee is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Employee does not exist",
                    Status = false
                };
            }

            var result = await _employeeRepository.DeleteEmployee(employee);
            if(result)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Employee deleted successfully",
                    Status = true,
                    
                };
            }

            return new BaseResponse<bool>
            {
                Message = $"Employee couldn't be deleted",
                Status = false,

            };

        }

        public async Task<BaseResponse<IList<EmployeeDto>>> GetAllEmployeesBySuperAdminAsync(string businessName)
        {
            var employees = await _employeeRepository.GetAllEmployeesBySuperAdminAsync(businessName);
            if (employees is null || !employees.Any())
            {
                return new BaseResponse<IList<EmployeeDto>>
                {
                    Message = $"Employees with Business Name: '{businessName}' do not exist",
                    Status = false,

                };
            }


            var employeeDtos = employees.Select(emp => new EmployeeDto
            {
                Email = emp.Email,
                FirstName = emp.Name,
                LastName = emp.LastName,
                Department = emp.Department,
                MobileNumber = emp.MobileNumber,
                EmployeeType = (Domain.Domain.Modules.Users.Entities.Enums.EmployeeType)emp.EmployeeType,
                UserType = emp.UserType,
                EmployeeUserId = emp.Id,
                EmailConfirmed = emp.EmailConfirmed,
                PincodeVerified = emp.PincodeVerified,
                CreatedBy = emp.CreatedBy,
                CreatedOn = emp.CreatedOn,
                Roles = emp.UserRoles.Select(r => new RoleDto
                {

                    RoleId = r.Id,
                    Name = r.Role.RoleName,
                    Description = r.Role.Description,
                    CreatedBy = r.CreatedBy,
                    Permissions = r.Role.RolePermissions.Where(rp => rp.RoleId == r.RoleId)
                    .Select(rp => new PermissionDto
                    {

                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name

                    }).ToList(),

                    SubPermissions = r.Role.RolePermissions.Where(rp => rp.RoleId == r.RoleId)
                    .Select(sp => new SubPermissionDto
                    {
                        Id = sp.SubPermission.Id,
                        Name = sp.SubPermission.Name,

                    }).ToList()
                }).ToList()

            }).ToList(); 

            return new BaseResponse<IList<EmployeeDto>>
            {
                Message = "Employees fetched successfully",
                Status = true,
                Data = employeeDtos
            };
        }

        public async Task<BaseResponse<User>> GetEmployeeByIdAsync(Guid employeeUserId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeUserId);
            if (employee is null)
            {
                return new BaseResponse<User>
                {
                    Message = $"Employee does not exist",
                    Status = false
                };
            }

            return new BaseResponse<User>
            {
                Message = $"Employee fetched successfully",
                Status = true,
                Data = employee
            };
        }

        public async Task<BaseResponse<User>> UpdateEmployeeAsync(Guid employeeUserId, UpdateEmployeeRequest model)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeUserId);
            if (employee is null)
            {
                return new BaseResponse<User>
                {
                    Message = $"Employee with Id: {employeeUserId} does not exist",
                    Status = false
                };
            }

            employee.Email = model.Email;
            employee.Name = model.Email;
            var updateEmployeeUser =  await _employeeRepository.UpdateEmployeeAsync(employee);
            if(updateEmployeeUser is null)
            {
                return new BaseResponse<User>
                {
                    Message = $"Employee couldn't be updated",
                    Status = false
                };
            }

            return new BaseResponse<User>
            {
                Message = $"Employee updated successfully",
                Status = true
            };
        }


        
        

        
    }
}
