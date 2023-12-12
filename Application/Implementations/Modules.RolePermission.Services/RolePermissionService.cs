using Application.DTOs;
using Application.Exceptions;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using Application.Interfaces;
using Domain.Entities.Enums;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Application.Interfaces.Modules.RolePermission.Repository;

namespace Application.Implementations.Modules.RolePermission.Services
{
    public class RolePermissionService : IRolePermissionService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IPermissionRepository _permissionRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ISubPermissionRepository _subPermissionRepository;
        private readonly IUserRepository _userRepository;
        private readonly IIdentityService _identityService;
        private readonly ILogger<RolePermissionService> _logger;

        public RolePermissionService(IRoleRepository roleRepository, IPermissionRepository permissionRepository, IRolePermissionsRepository rolePermissionsRepository, ISubPermissionRepository subPermissionRepository, IUserRepository userRepository, IIdentityService identityService, ILogger<RolePermissionService> logger)
        {
            _roleRepository = roleRepository;
            _permissionRepository = permissionRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _subPermissionRepository = subPermissionRepository;
            _userRepository = userRepository;
            _identityService = identityService;
            _logger = logger;
        }

        public async Task<BaseResponse<Role>> AddRoleAsync(string superAdminUserToken, CreateRoleRequestModel model)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(superAdminUserToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                throw new BadRequestException("User not authenticated");
            }

            //if (!user.EmailConfirmed) throw new BadRequestException("User unverified");

            //if (!user.PincodeVerified) return new BaseResponse { Message = "pincode not verified", Status = false };

            if (user.UserType != UserType.SuperAdmin) throw new BadRequestException("Unauthorized!");
            if (model is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Role>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            if(model == null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Role>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }
            var roleExist = await _roleRepository.RoleExistsBySuperAdmin(model.Name, user.BusinessName);
            if (roleExist)
            {
                _logger.LogError($"The role name: '{model.Name}' already exists!");
                return new BaseResponse<Role>
                {
                    Message = $"The role name: '{model.Name}' already exists!",
                    Status = false
                };
            }

            // Create the role entity
            var role = new Role
            {
                RoleName = model.Name,
                Description = model.Description,
                CreatedBy = user.BusinessName
            };

            // Get the permissions and subpermissions based on the selected IDs
            var permissions = await _permissionRepository.GetPermissionsByIdsAsync(model.SelectedPermissionIds);
            var subPermissions = await _subPermissionRepository.GetSubPermissionsByIdsAsync(model.SelectedSubPermissionIds);

            // Create RolePermission entities for permissions
            var rolePermissions = permissions.Select(permission => new Domain.Domain.Modules.RolePermission.Entities.RolePermission
            {
                RoleId = role.Id,
                PermissionId = permission.Id
            }).ToList();

            // Create RolePermission entities for subpermissions
            foreach (var subPermission in subPermissions)
            {
                rolePermissions.Add(new Domain.Domain.Modules.RolePermission.Entities.RolePermission
                {
                    RoleId = role.Id,
                    PermissionId = subPermission.PermissionId
                });
            }

            // Save the role to the database
            var newRole = await _roleRepository.AddRoleAsync(role);

            if (newRole is null)
            {
                _logger.LogInformation("Role creation unsuccessful");
                return new BaseResponse<Role>
                {
                    Message = "Role creation unsuccessful",
                    Status = false
                };
            }

            // Save the role permissions to the database
            await _rolePermissionsRepository.AddRolePermissionsAsync(rolePermissions);

            _logger.LogInformation($"Role with name: {role.RoleName} created, and permissions have been successfully assigned");
            return new BaseResponse<Role>
            {
                Message = $"Role with name: {role.RoleName} created, and permissions have been successfully assigned",
                Status = true
            };
        }

        public async Task<BaseResponse<Role>> CloneRoleAsync(Guid roleId)
        {
            // Retrieve the original role to be cloned
            var originalRole = await _roleRepository.GetRoleByIdAsync(roleId);

            if (originalRole == null)
            {

                _logger.LogInformation($"Role with ID {roleId} not found.");
                return new BaseResponse<Role>
                {
                    Message = $"Role with ID {roleId} not found.",
                    Status = false
                };

            }

            var clonedRole = new Role
            {
                RoleName = originalRole.RoleName + "_Clone",
                Description = originalRole.Description + " (Cloned)",
                CreatedBy = originalRole.CreatedBy
            };

            var newRole = await _roleRepository.AddRoleAsync(clonedRole);

            foreach (var originalRolePermission in originalRole.RolePermissions)
            {
                var clonedRolePermission = new Domain.Domain.Modules.RolePermission.Entities.RolePermission
                {
                    RoleId = newRole.Id,
                    PermissionId = originalRolePermission.PermissionId
                };

                await _rolePermissionsRepository.AddRolePermissionAsync(clonedRolePermission);
            }

            _logger.LogInformation($"Role with name: {originalRole.RoleName} cloned successfully");
            return new BaseResponse<Role>
            {
                Message = $"Role with name: {originalRole.RoleName} cloned successfully",
                Status = true
            };


        }

        public async Task<BaseResponse<IList<BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>>>> GetAllRolePermissionsAsync()
        {
            var rolePermissions = await _rolePermissionsRepository.GetAllRolePermissionsAsync();
            var responseRolePermissions = rolePermissions.Select(rolePermission => new BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>
            {
                Message = $"Role Permissions fetched successfully",
                Status = true,
                Data = rolePermission
            }).ToList();

            

            return new BaseResponse<IList<BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>>>
            {
                Message = "Role Permissions fetched successfully",
                Status = true,
                Data = responseRolePermissions
            };
        }

        public async Task<BaseResponse<IList<BaseResponse<Permission>>>> GetPermissionsByRoleAsync(Guid roleId)
        {
            var permissions = await _rolePermissionsRepository.GetPermissionsByRoleAsync(roleId);
            var responsePermissions = permissions.Select(permission => new BaseResponse<Permission>
            {
                Message = $"Role Permissions fetched successfully",
                Status = true,
                Data = permission
            }).ToList();

            return new BaseResponse<IList<BaseResponse<Permission>>>
            {
                Message = "Role Permissions fetched successfully",
                Status = true,
                Data = responsePermissions
            };
        }

        public async Task<BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> GetRolePermissionByIdAsync(Guid rolePermissionId)
        {
            var rolePermission = await _rolePermissionsRepository.GetRolePermissionByIdAsync(rolePermissionId);

            if (rolePermission is null)
            {
                return new BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>
                {
                    Message = $"Role permission with Id: {rolePermission.Id} does not exist",
                    Status = false,
                };

            }

            return new BaseResponse<Domain.Domain.Modules.RolePermission.Entities.RolePermission>
            {
                Message = $"Data fecthed successfully",
                Status = true,
                Data = rolePermission
            };
        }

        public async Task<BaseResponse<bool>> RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);

            if(role is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Role with Id: {roleId} does not exist",
                    Status = false,
                };
            }
            var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);

            if(permission is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Permission with Id: {permissionId} does not exist",
                    Status = false,
                };
            }

            await _rolePermissionsRepository.RemovePermissionFromRoleAsync(roleId, permissionId);
            return new BaseResponse<bool>
            {
                Message = $"Permission with name: {permission.Name} successfully removed from Role",
                Status = true
            };
             
            
        }

        public async Task<BaseResponse<Role>> UpdateRoleAsync(Guid roleId, UpdateRoleRequestModel model)
        {
            var existingRole = await _roleRepository.GetRoleByIdAsync(roleId);

            if (existingRole == null)
            {
                return new BaseResponse<Role>
                {
                    Message = "Role not found",
                    Status = false
                };
            }

            // Update the role properties based on the model
            existingRole.RoleName = model.Name;
            existingRole.Description = model.Description;

            // Clear the role premission table
            existingRole.RolePermissions.Clear();

            // Get the permissions and subpermissions based on the selected IDs
            var permissions = await _permissionRepository.GetPermissionsByIdsAsync(model.SelectedPermissionIds);
            var subPermissions = await _subPermissionRepository.GetSubPermissionsByIdsAsync(model.SelectedSubPermissionIds);

            // Create RolePermission entities for permissions
            var rolePermissions = permissions.Select(permission => new Domain.Domain.Modules.RolePermission.Entities.RolePermission
            {
                RoleId = existingRole.Id,
                PermissionId = permission.Id
            }).ToList();

            // Create RolePermission entities for subpermissions
            foreach (var subPermission in subPermissions)
            {
                rolePermissions.Add(new Domain.Domain.Modules.RolePermission.Entities.RolePermission
                {
                    RoleId = existingRole.Id,
                    PermissionId = subPermission.PermissionId
                });
            }

            // Save the role to the database
            var updateRole = await _roleRepository.UpdateRoleAsync(existingRole);

            if (updateRole is null)
            {
                _logger.LogInformation("Role update unsuccessful");
                return new BaseResponse<Role>
                {
                    Message = "Role update unsuccessful",
                    Status = false
                };
            }

            // Save the role permissions to the database
            await _rolePermissionsRepository.AddRolePermissionsAsync(rolePermissions);

            _logger.LogInformation($"Role with name: {existingRole.RoleName} updated successfully with permissions");
            return new BaseResponse<Role>
            {
                Message = $"Role with name: {existingRole.RoleName} updated successfully with permissions",
                Status = true
            };
        }

        public async Task<BaseResponse<bool>> UserHasPermissionAsync(Guid userId, string permissionName)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if(user is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"User with Id: {userId} does not exist",
                    Status = false,
                };
            }
            var permission = await _permissionRepository.GetAsync(pm => pm.Name == permissionName);

            if (permission is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Permission with name: {permissionName} does not exist",
                    Status = false,
                };
            }
            var result = await _rolePermissionsRepository.UserHasPermissionAsync(userId, permissionName);

            if (result)
            {
                return new BaseResponse<bool>
                {
                    Message = $"User has permission",
                    Status = true,
                };
            }
            return new BaseResponse<bool>
            {
                Message = $"User doesnt have permission",
                Status = false,
            };
        }

        public async Task<BaseResponse<bool>> UserHasPermissionAndSubPermissionAsync(Guid userId, List<string> permissionNames, List<string> subPermissionNames)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"User with Id: {userId} does not exist",
                    Status = false,
                };
            }

            Permission permission = null;
            foreach (var name in permissionNames)
            {
                permission = await _permissionRepository.GetAsync(pm => pm.Name == name);
            }   

            if (permission is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Permission with name: {permission.Name} does not exist",
                    Status = false,
                };
            }

            SubPermission subPermission = null;

            foreach(var  name in subPermissionNames)
            {
                subPermission = await _subPermissionRepository.GetAsync(sp => sp.Name == name);
            }

            if (subPermission is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"SubPermission with name: {subPermission.Name} does not exist",
                    Status = false,
                };
            }

            var result = await _rolePermissionsRepository.UserHasPermissionAndSubPermissionAsync(userId, permissionNames, subPermissionNames);

            if (result)
            {
                return new BaseResponse<bool>
                {
                    Message = $"User has permission",
                    Status = true,
                };
            }
            return new BaseResponse<bool>
            {
                Message = $"User doesnt have permission",
                Status = false,
            };
        }
    }
}
