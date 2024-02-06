using Application.DTOs;
using Application.DTOs.RequestModel;
using Application.DTOs.RequestModel.User;
using Application.Exceptions;
using Application.Interfaces;
using Application.Interfaces.Modules.Users.IService;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Domain.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.Users.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IMailService _mailService;
        private readonly ILogger<UserService> _logger;
        private readonly IConfiguration _configuration;

        public UserService(UserManager<User> userManager, RoleManager<Role> roleManager, IIdentityService identityService, IUserRepository userRepository, IMailService mailService, ILogger<UserService> logger, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _identityService = identityService;
            _userRepository = userRepository;
            _mailService = mailService;
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<BaseResponse> AddKycAsync(KYCRequestModel request)
        {
            var superAdmin = await _userRepository.GetUserByIdAsync(request.SuperAdminId);
            if (superAdmin is null) return new BaseResponse
            {
                Message = "super admin does not exist",
                Status = false
            };
            if (superAdmin.UserType != UserType.SuperAdmin) return new BaseResponse
            {
                Message = "You are not permitted to perform this task",
                Status = false
            };

            var kycExist = await _userRepository.KycExistsAsync(k => k.CompanyName == request.CompanyName);
            if (kycExist)
            {
                throw new BadRequestException($"KYC with {request.CompanyName} Already Exist!");
            }

            if(request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var kyc = new KYC
            {
                SuperAdminId = superAdmin.Id,
                CompanyName = request.CompanyName,
                AccountHolderOrBusinessName = request.AccountHolderOrBusinessName,
                BankCountry = request.BankCountry,
                BusinessAddressLine1 = request.BusinessAddressLine1,
                BusinessAddressLine2 = request.BusinessAddressLine2,
                BusinessFiscalYearFrom = request.BusinessFiscalYearFrom,
                BusinessFiscalYearTo = request.BusinessFiscalYearTo,
                BusinessPhoneNumber = request.BusinessPhoneNumber,
                Category = request.Category,
                BVN = request.BVN,
                City = request.City,
                Country = request.Country,
                FirstName = request.FirstName,
                LastName = request.LastName,
                RegisteredCity = request.RegisteredCity,
                RegisteredHomeAddress = request.RegisteredHomeAddress,
                VatNumber = request.VatNumber,
                WebPage = request.WebPage
            };

            var createKyc = await _userRepository.AddKYCAsync(kyc);
            if (createKyc == null) return new BaseResponse
            {
                Message = "Kyc creation unsuccessful",
                Status = false
            };

            return new BaseResponse
            {
                Message = "Kyc successfully created",
                Status = true
            };
           
        }

        public async Task<BaseResponse> AddUserAsync(CreateUserAdmin request)
        {
            var userNameExist = await _userRepository.AnyAsync(u => u.BusinessName == request.BusinessName);
            if (userNameExist)
            {
                throw new BadRequestException("User Already Exist!");
            }

            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var user = new User
            {
                Email = request.BusinessEmail,
                BusinessName = request.BusinessName,
                UserType = Domain.Entities.Enums.UserType.SuperAdmin,
                CreatedBy = request.BusinessEmail,
                CreatedOn = DateTime.UtcNow
            };

            if (request.PasswordHash != request.ConfirmPassword) return new BaseResponse
            {
                Message = "Password doesnt match!",
                Status = false,
            };

            if (request.Pincode != request.ConfirmPincode) return new BaseResponse
            {
                Message = "Pincode doesnt match!",
                Status = false,
            };

            (var passwordResult, var message) = ValidatePassword(request.PasswordHash);
            if (!passwordResult) return new BaseResponse { Message = message, Status = false };

            user.PasswordHash = _identityService.GetPasswordHash(request.PasswordHash);
            user.PinCode = _identityService.GetPincodeHash(request.Pincode);

            
            var newUser = await _userManager.CreateAsync(user);
            if (newUser == null)
            {
                return new BaseResponse
                {
                    Message = "User Creation Unsuccessful",
                    Status = true
                };
            }   

            var result = await _userManager.AddToRoleAsync(user, "SuperAdmin");
            if (!result.Succeeded)
            {
                throw new Exception("Unable to add user to the 'SuperAdmin' role");
            }

            var roles = await _userManager.GetRolesAsync(user);
           
            var token = _identityService.GenerateToken(user, roles);

            //Send verification mail
            //var sent = await _mailService.SendVerificationMail(user.Email, user.BusinessName,  token);
            //if (!sent)
            //{
            //    _logger.LogWarning($"User created, but unable to Send Verification Email to user with Email {user.Email}");
            //    return new BaseResponse
            //    {
            //        Message = $"User created, but unable to Send Verification Email to user with Email {user.Email}",
            //        Status = false
            //    };
            //}

            _logger.LogWarning($"User created successfully");
            return new BaseResponse
            {
                Message = $"A verification mail has been sent to {user.Email}",
                Status = true
            };

            //return new BaseResponse
            //{
            //    Message = $"A User with email: {user.Email} has been created successfully",
            //    Status = true
            //};




        }


        public async Task<SuperAdminUserResponseModel> GetUserAsync(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if(user.UserType == UserType.Employee)
            {
                return new SuperAdminUserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserType =UserType.Employee,
                    EmailConfirmed = user.EmailConfirmed,
                  
                    Roles = user.UserRoles.Select(r => new RoleDto
                    {
                        RoleId = r.Role.Id,
                        Name = r.Role.RoleName,
                        Description = r.Role.Description
                    }).ToList(),
                    Status = true,
                    Message = "Data fetched successfully"
                };
            }


            return new SuperAdminUserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                BusinessName = user.BusinessName,
                UserType = UserType.SuperAdmin,
                EmailConfirmed = user.EmailConfirmed,

                Roles = user.UserRoles.Select(r => new RoleDto
                {
                    RoleId = r.Role.Id,
                    Name = r.Role.RoleName
                }).ToList(),
                Status = true,
                Message = "Data fetched successfully"
            };

        }

        //public Task<List<SuperAdminUserResponseModel>> GetUsersAsync()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IList<UsersInRoleResponseModel>> GetUsersByRoleAsync(string roleName)
        //{
        //    throw new NotImplementedException();
        //}


        public async Task<TokenResponseData> CreateToken(TokenRequestModel model)
        {
            // check if user exits
            var user = await _userManager.FindByEmailAsync(model.Email);
           
            if(user is null) throw new BadRequestException($"User with {model.Email} does not exist!");

            // Check if user has been verified
            if(!user.EmailConfirmed) throw new BadRequestException($"User with {model.Email} has not been verified");

            var result = await _userManager.CheckPasswordAsync(user, model.Password);
            if (!result) throw new BadRequestException("Incorrect email or password");

            // generate token 
            var roles = user.UserRoles.Select(r => r.Role.RoleName).ToArray();
            var token = _identityService.GenerateToken(user, roles);

            if(user.UserType == UserType.SuperAdmin)
            {
                var data = new TokenResponseData
                {
                    BusinessEmail = user.Email,
                    BusinessName = user.BusinessName,
                    Id = user.Id,
                    UserType = UserType.SuperAdmin,
                    RoleName = String.Join(",", roles),
                    Token = token
                    
                };

                return data;
            }
            var employeeData = new TokenResponseData
            {
                Email = user.Email,
                Id = user.Id,
                UserType = UserType.Employee,
                RoleName = string.Join(",", roles),
                Token = token
            };
            
            return employeeData;
        }

        public async Task<VerifyUserResponse> VerifyUser(string token)
        {
            var claims = _identityService.ValidateToken(token);
            if (claims == null) throw new BadRequestException("Unable to validate with the provided token");
            var email = claims.SingleOrDefault(c => c.Type == "email");
            if (email == null) throw new BadRequestException("User Claims not valid");
            var user = await _userRepository.GetUserByEmail(email.Value);
            if (user == null) throw new BadRequestException("Unable to find user");
            if (user.EmailConfirmed) throw new BadRequestException("User already Validated");
            await _userRepository.UpdateUserToVerifiedAsync(user.Id, user);

            if(user.UserType == UserType.SuperAdmin)
            {
                return new VerifyUserResponse
                {
                    Status = true,
                    Message = "User validated successfully",
                    BusinessName = user.BusinessName,
                    Email = user.Email,
                    UserId = user.Id,
                };
            }

            return new VerifyUserResponse
            {
                
                Status = true,
                Message = "User validated successfully",
                Email = user.Email,
                FirstName = user.Name,
                BusinessName = user.CreatedBy,
                UserId = user.Id,
                EmployeeType = (Domain.Domain.Modules.Users.Entities.Enums.EmployeeType)user.EmployeeType,

            };




        }

        public async Task<BaseResponse> VerifyToken(string token)
        {
            var claims = _identityService.ValidateToken(token);
            if (claims == null) throw new BadRequestException("Unable to validate with the provided token");
            var email = claims.SingleOrDefault(c => c.Type == "email");
            if (email == null) throw new BadRequestException("User Claims not valid");
            var user = await _userRepository.GetUserByEmail(email.Value);
            if (user == null) throw new BadRequestException("Unable to find user");

            return new BaseResponse
            {
                Status = true,
                Message = "Token verified"
            };
        }

        public async Task<BaseResponse> VerifyPinCode(string token, string userPincode)
        {
            // Get logged in user
            var claims = _identityService.ValidateToken(token);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);

            if (user is null) throw new BadRequestException("User cannot be found!");
            if (string.IsNullOrEmpty(userPincode)) throw new ArgumentNullException(nameof(userPincode));
            var verify = _identityService.VerifyPincode(user, userPincode);
            if (!verify) throw new BadRequestException("Invalid pincode");
            await _userRepository.UpdatePincodeVerifiedStatus(user.Id, true);
            return new BaseResponse { Message = "Pincode verified", Status = true};
        }

        public async Task<BaseResponse> ChangePincode(ChangePincode request)
        {

            var email = _identityService.GetClaimValue(ClaimTypes.Email);

            // get user 
            var user = await _userRepository.GetUserByEmail(email);
            if (user == null) return new BaseResponse { Message = "User not found", Status = false };

            // check if password match
            if (request.Pincode != request.ConfirmPincode) return new BaseResponse
            {
                Message = "pincode does not match",
                Status = false
            };

            var pincodeHash = _identityService.GetPincodeHash(request.Pincode);

            // update password
            user.ChangePassword(pincodeHash);
            user.LastModifiedOn = DateTime.UtcNow;

            //update database
            await _userRepository.UpdateUserAsync(user);
            return new BaseResponse { Message = "Pincode reset successfully", Status = true };


        }

        public async Task<BaseResponse> ResetPasswordAndPincode(string token, ResetPasswordAndPincodeRequest request)
        {

            var claims = _identityService.ValidateToken(token);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            // get user 
            var user = await _userRepository.GetUserByEmail(email.Value);
            if (user == null) return new BaseResponse { Message = "User not found", Status = false };

            // check if password match
            if (request.Password != request.ConfirmPassword) return new BaseResponse
            {
                Message = "password does not match",
                Status = false
            };

            if (request.Pincode != request.ConfirmPincode) return new BaseResponse
            {
                Message = "pincode does not match",
                Status = false
            };

            (var result, var message) = ValidatePassword(request.Password);
            if (!result) return new BaseResponse { Message = message, Status = false };

            var passwordHash = _identityService.GetPasswordHash(request.Password);
            var pincodeHash = _identityService.GetPincodeHash(request.Pincode);

            // update password
            user.ChangePassword(passwordHash);
            user.ChangePincode(pincodeHash);
            user.LastModifiedOn = DateTime.UtcNow;

            //update database
            await _userRepository.UpdateUserAsync(user);
            return new BaseResponse { Message = "Password and Pincode reset successfully", Status = true };

        }

        public async Task<BaseResponse> PasswordReset(string token, PasswordResetRequestModel request)
        {
            var claims = _identityService.ValidateToken(token);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            // get user 
            var user = await _userRepository.GetUserByEmail(email.Value);
            if (user == null) return new BaseResponse { Message = "User not found", Status = false };

            // check if password match
            if (request.Password != request.ConfirmPassword) return new BaseResponse
            {
                Message = "password does not match",
                Status = false
            };

            (var result, var message) = ValidatePassword(request.Password);
            if (!result) return new BaseResponse { Message = message, Status = false };

            var passwordHash = _identityService.GetPasswordHash(request.Password);

            // update password
            user.ChangePassword(passwordHash);
            user.LastModifiedOn = DateTime.UtcNow;

            //update database
            await _userRepository.UpdateUserAsync(user);
            return new BaseResponse { Message = "Password reset successfully", Status = true };
        }

        public async Task<BaseResponse> ForgotPassword(ForgotPasswordRequest request)
        {
            var user = await _userRepository.GetUserByEmail(request.Email);
            if (user == null) return new BaseResponse { Message = "User not found", Status = false };

            // generate token
            var token = _identityService.GenerateToken(user, Array.Empty<string>());
            // form link
            var resetLink = $"{_configuration.GetSection("Urls:BaseUrl").Value}/{_configuration.GetSection("Urls:PasswordReset").Value}{token}";

            // send mail
            var result = await _mailService.SendForgotPasswordMail(user.Email, user.Name, resetLink);
            if (!result)
            {
                _logger.LogWarning("Unable to send reset mail!");
                return new BaseResponse
                {
                    Message = "Unable to send reset mail!",
                    Status = false
                };
            }

            _logger.LogWarning("A email with the reset link has been sent to your email!");
            return new BaseResponse
            {
                Message = "A email with the reset link has been sent to your email!",
                Status = true
            };


        }

        public async Task<BaseResponse> GetUserById(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            //var roles = user.UserRoles.Select(r => r.Role.RoleName).ToArray();
            var roles = await _userManager.GetRolesAsync(user);
            if (user.UserType == UserType.Employee)
            {
                return new SuperAdminUserResponseModel
                {
                    Id = user.Id,
                    Email = user.Email,
                    UserType =UserType.Employee,
                  
                    RoleName = string.Join(",", roles),
                    Status = true,
                    Message = "Data fetched successfully"
                };
            }

            return new SuperAdminUserResponseModel
            {
                Id = user.Id,
                Email = user.Email,
                BusinessName = user.BusinessName,
                UserType =UserType.SuperAdmin,
                
                Roles = user.UserRoles.Select(r => new RoleDto
                {
                    RoleId = r.Role.Id,
                    Name = r.Role.RoleName
                }).ToList(),
                Status = true,
                Message = "Data fetched successfully"
            };
        }

        public async Task<BaseResponse> KycExistsAsync(Guid superAdminId)
        {
           var kycExists =  await _userRepository.KycExistsAsync(kyc => kyc.SuperAdminId == superAdminId);
            if (!kycExists) return new BaseResponse { Message = "Kyc does not exist", Status = false };
            return new BaseResponse { Message = "Kyc exists", Status = true };
        }
        

        private static (bool, string?) ValidatePassword(string password)
        {
            // Minimum length of password
            int minLength = 8;

            // Maximum length of password
            int maxLength = 50;

            // Check for null or empty password
            if (string.IsNullOrEmpty(password))
            {
                return (false, "Password cannot be null or empty.");
            }

            // Check length of password
            if (password.Length < minLength || password.Length > maxLength)
            {
                return (false, $"Password must be between {minLength} and {maxLength} characters long.");
            }

            // Check for at least one uppercase letter, one lowercase letter, and one digit
            bool hasUppercase = false;
            bool hasLowercase = false;
            bool hasDigit = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c))
                {
                    hasUppercase = true;
                }
                else if (char.IsLower(c))
                {
                    hasLowercase = true;
                }
                else if (char.IsDigit(c))
                {
                    hasDigit = true;
                }
            }

            if (!hasUppercase || !hasLowercase || !hasDigit)
            {
                return (false, "Password must contain at least one uppercase letter, one lowercase letter, and one digit.");
            }

            // Check for any characters
            string invalidCharacters = @" !""#$%&'()*+,-./:;<=>?@[\\]^_`{|}~";
            if (password.IndexOfAny(invalidCharacters.ToCharArray()) == -1)
            {
                return (false, "Password must contains one or more characters.");
            }

            // Password is valid
            return (true, null);
        }

        public async Task<BaseResponse> DeleteUserAsync(Guid userId)
        {
            var checkUser = await _userRepository.GetUserByIdAsync(userId);
            if(checkUser is null)
            {
                return new BaseResponse
                {
                    Message = $"User with Id: {userId} cannot be found",
                    Status = false
                };
            }

            if (checkUser.UserType == UserType.SuperAdmin)
            {
                return new BaseResponse
                {
                    Message = "The Super Admin data cannot be deleted",
                    Status = false
                };
            }
            await _userRepository.DeleteUser(checkUser);
            return new BaseResponse
            {
                Message = $"User with Id: {userId} deleted successfully.",
                Status = true
            };

        }

        
    }
    
}
