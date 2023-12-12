using Application.DTOs;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Domain.Domain.Modules.RolePermission.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.RolePermission.Services
{
    public class PermissionService : IPermissionService
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly ISubPermissionRepository _subPermissionRepository;

        public PermissionService(IPermissionRepository permissionRepository, ISubPermissionRepository subPermissionRepository)
        {
            _permissionRepository = permissionRepository;
            _subPermissionRepository = subPermissionRepository;
        }

        //public async Task<BaseResponse<IList<BaseResponse<Permission>>>> GetAllPermissionsAsync()
        //{
        //    var permissions = await _permissionRepository.GetAllPermissionsAsync();
        //    var responsePermissions = permissions.Select(permission => new BaseResponse<Permission>
        //    {
        //        Message = $"Permission '{permission.Name}' fetched successfully",
        //        Status = true,
        //        Data = permission
        //    }).ToList();

        //    return new BaseResponse<IList<BaseResponse<Permission>>>
        //    {
        //        Message = "Permissions fetched successfully",
        //        Status = true,
        //        Data = responsePermissions
        //    };
        //}

        public async Task<BaseResponse<Permission>> GetPermissionByIdAsync(Guid permissionId)
        {
            var permission = await _permissionRepository.GetPermissionByIdAsync(permissionId);
            if (permission is null)
            {
                return new BaseResponse<Permission>
                {
                    Message = $"Permission with Id: {permissionId} does not exist",
                    Status = false
                };
            }

            return new BaseResponse<Permission>
            {
                Message = $"Permission with Id: {permissionId} fetched successfully",
                Status = true,
                Data = permission
            };

        }

        public async Task<BaseResponse<Permission>> GetPermissionByNameAsync(string permissionName)
        {
            var permission = await _permissionRepository.GetAsync(pm => pm.Name == permissionName);
            if (permission is null)
            {
                return new BaseResponse<Permission>
                {
                    Message = $"Permission with name: {permissionName} does not exist",
                    Status = false
                };
            }

            return new BaseResponse<Permission>
            {
                Message = $"Permission with name: {permissionName} fetched successfully",
                Status = true,
                Data = permission
            };
        }

        public async Task<BaseResponse<IList<BaseResponse<Permission>>>> GetPermissionsByIdsAsync(List<Guid> permissionIds)
        {
            var permissions = await _permissionRepository.GetPermissionsByIdsAsync(permissionIds);
            var responsePermissions = permissions.Select(permission => new BaseResponse<Permission>
            {
                Message = $"Permission '{permission.Name}' fetched successfully",
                Status = true,
                Data = permission
            }).ToList();

            return new BaseResponse<IList<BaseResponse<Permission>>>
            {
                Message = "Permissions fetched successfully",
                Status = true,
                Data = responsePermissions
            };
        }

        //public async Task<BaseResponse<Dictionary<string, Dictionary<string, object>>>> GetAllPermissionsAsync()
        //{
        //    var permissions = await _permissionRepository.GetAllPermissionsAsync();

        //    // Create a dictionary to hold grouped permissions
        //    var groupedPermissions = new Dictionary<string, Dictionary<string, object>>();

        //    // Track the current parent permission
        //    string currentParent = null; 

        //    foreach (var permission in permissions)
        //    {
        //        if (permission.Name == "Till")
        //        {
        //            // If the permission is "Till," nest it under the current parent (e.g., "Cash Register")
        //            if (!string.IsNullOrEmpty(currentParent))
        //            {
        //                if (!groupedPermissions.ContainsKey(currentParent))
        //                {
        //                    groupedPermissions[currentParent] = new Dictionary<string, object>();
        //                }

        //                var parentPermission = groupedPermissions[currentParent];
        //                if (!parentPermission.ContainsKey("Till"))
        //                {
        //                    parentPermission["Till"] = new Dictionary<string, List<SubPermissionDto>>();
        //                }

        //                var tillSubPermissions = parentPermission["Till"] as Dictionary<string, List<SubPermissionDto>>;
        //                if (permission.SubPermissions.Any())
        //                {
        //                    tillSubPermissions["SubPermissions"] = permission.SubPermissions
        //                        .Select(sub => new SubPermissionDto
        //                        {
        //                            Id = sub.Id,
        //                            Name = sub.Name
        //                        }).ToList();
        //                }
        //            }
        //        }
        //        else
        //        {
        //            // Handle other permissions (not "Till")
        //            // Update the current parent
        //            currentParent = permission.Name; 
        //            if (!groupedPermissions.ContainsKey(permission.Name))
        //            {
        //                groupedPermissions[permission.Name] = new Dictionary<string, object>();
        //            }

        //            var subPermissionGroup = groupedPermissions[permission.Name];
        //            if (permission.SubPermissions.Any())
        //            {
        //                subPermissionGroup["SubPermissions"] = permission.SubPermissions
        //                    .Select(sub => new SubPermissionDto
        //                    {
        //                        Id = sub.Id,
        //                        Name = sub.Name
        //                    }).ToList();
        //            }
        //        }
        //    }

        //    return new BaseResponse<Dictionary<string, Dictionary<string, object>>>
        //    {
        //        Message = "Permissions grouped successfully",
        //        Status = true,
        //        Data = groupedPermissions
        //    };
        //}

        public async Task<BaseResponse<Dictionary<string, Dictionary<string, object>>>> GetAllPermissionsAsync()
        {
            var permissions = await _permissionRepository.GetAllPermissionsAsync();

            // Create a dictionary to hold grouped permissions
            var groupedPermissions = new Dictionary<string, Dictionary<string, object>>();

            // Track the current parent permission
            string currentParent = null;

            foreach (var permission in permissions)
            {
                if (permission.Name == "Till")
                {
                    
                    if (!string.IsNullOrEmpty(currentParent))
                    {
                        if (!groupedPermissions.ContainsKey(currentParent))
                        {
                            groupedPermissions[currentParent] = new Dictionary<string, object>();
                        }

                        var parentPermission = groupedPermissions[currentParent];
                        if (!parentPermission.ContainsKey("Till"))
                        {
                            parentPermission["Till"] = new Dictionary<string, object>
                    {
                            {"PermissionId", permission.Id},
                            {"PermissionName", permission.Name},
                            {"SubPermissions", new List<SubPermissionDto>()}
                    };
                        }

                        var tillSubPermissions = parentPermission["Till"] as Dictionary<string, object>;
                        if (permission.SubPermissions.Any())
                        {
                            tillSubPermissions["SubPermissions"] = permission.SubPermissions
                                .Select(sub => new SubPermissionDto
                                {
                                    Id = sub.Id,
                                    Name = sub.Name
                                }).ToList();
                        }
                    }
                }
                else
                {
                    // Handle other permissions (not "Till")
                    // Update the current parent
                    currentParent = permission.Name;
                    if (!groupedPermissions.ContainsKey(permission.Name))
                    {
                        groupedPermissions[permission.Name] = new Dictionary<string, object>
                {
                        {"PermissionId", permission.Id},
                        {"PermissionName", permission.Name},
                        {"SubPermissions", new List<SubPermissionDto>()}
                };
                    }

                    var subPermissionGroup = groupedPermissions[permission.Name];
                    if (permission.SubPermissions.Any())
                    {
                        subPermissionGroup["SubPermissions"] = permission.SubPermissions
                            .Select(sub => new SubPermissionDto
                            {
                                Id = sub.Id,
                                Name = sub.Name
                            }).ToList();
                    }
                }
            }

            return new BaseResponse<Dictionary<string, Dictionary<string, object>>>
            {
                Message = "Permissions grouped successfully",
                Status = true,
                Data = groupedPermissions
            };
        }




    }
}
