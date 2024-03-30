using Application.DTOs;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.RolePermission.Services
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;

        public RoleService(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<BaseResponse<bool>> DeleteRole(Guid roleId)
        {
            var role = await _roleRepository.GetRoleByIdAsync(roleId);
            if (role is null)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Role does not exist",
                    Status = false
                };
            }

            var result = await _roleRepository.DeleteRole(role);
            if (result)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Role deleted successfully",
                    Status = true,

                };
            }

            return new BaseResponse<bool>
            {
                Message = $"Role couldn't be deleted",
                Status = false,

            };
        }

        public async Task<BaseResponse<Role>> GetRoleAsync(string name)
        {
            var role = await _roleRepository.GetRoleByName(name);

            if (role is null)
            {

                return new BaseResponse<Role>
                {
                    Message = $"Role with name: {name} does not exist",
                    Status = false,
                };

            }

            return new BaseResponse<Role>
            {
                Message = "Data fetched successfully",
                Status = true,
                Data = role

            };
        }

        public async Task<BaseResponse<Role>> GetRoleByIdAsync(Guid roleId)
        {
            var role =  await _roleRepository.GetRoleByIdAsync(roleId);
            
            if(role is null)
            {
                
                return new BaseResponse<Role>
                {
                    Message = $"Role with Id: {roleId} does not exist",
                    Status = false,
                };
                
            }

            return new BaseResponse<Role>
            {
                Message = "Data fetched successfully",
                Status = true,
                Data = role

            };
        }

        public async Task<BaseResponse<IList<RoleDto>>> GetRolesBySuperAdminAsync(string businessName)
        {
            var roles = await _roleRepository.GetAllRolesBySuperAdminAsync(businessName);

            if (roles is null || !roles.Any())
            {
                return new BaseResponse<IList<RoleDto>>
                {
                    Message = $"Roles with Business Name: '{businessName}' do not exist",
                    Status = false,

                };

            }


            var roleDTOs = roles.Select(r => new RoleDto
            {
                RoleId = r.Id,
                Name = r.RoleName,
                Description = r.Description,
                CreatedBy = r.CreatedBy,
                Permissions = r.RolePermissions.Where(rp => rp.RoleId == r.Id)
                .Select(rp => new PermissionDto
                {
                   
                        Id = rp.Permission.Id,
                        Name = rp.Permission.Name,
                        
                }).ToList(),
                SubPermissions = r.RolePermissions.Where(rp => rp.RoleId == r.Id)
                .Select(sp => new SubPermissionDto
                {
                    Id = sp.SubPermission.Id,
                    Name = sp.SubPermission.Name,

                }).ToList()
            }).ToList();

            return new BaseResponse<IList<RoleDto>>
            {
                Message = "Roles fetched successfully",
                Status = true,
                Data = roleDTOs
            };
        }

        public async Task<BaseResponse<bool>> RoleExistsAsync(string name, string businessName)
        {
            var roleExists = await _roleRepository.RoleExistsBySuperAdmin(name, businessName);
            if (roleExists)
            {
                return new BaseResponse<bool>
                {
                    Message = $"Role with name: {name} exists",
                    Status = true,
                };
            }

            return new BaseResponse<bool>
            {
                Message = $"Role with name: {name} does not exist",
                Status = false,
            };
        }

       
    }
}
