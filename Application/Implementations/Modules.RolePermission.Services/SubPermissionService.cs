using Application.DTOs;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Modules.RolesAndPermissions.Service;
using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Implementations.Modules.RolePermission.Services
{
    public class SubPermissionService : ISubPermissionService
    {
        private readonly ISubPermissionRepository _subPermissionRepository;
        private readonly ILogger<SubPermissionService> _logger;

        public SubPermissionService(ISubPermissionRepository subPermissionRepository, ILogger<SubPermissionService> logger)
        {
            _subPermissionRepository = subPermissionRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<SubPermission>> GetSubPermissionByIdAsync(Guid subPermissionId)
        {
            var subPermission = await _subPermissionRepository.GetSubPermissionByIdAsync(subPermissionId);

            if (subPermission is null)
            {
                 _logger.LogError($"SubPermission with Id: {subPermissionId} does not exist");
                return new BaseResponse<SubPermission>
                {
                    Message = $"SubPermission with Id: {subPermissionId} does not exist",
                    Status = false,
                };

            }

            _logger.LogInformation($"Data fetched successfully");
            return new BaseResponse<SubPermission>
            {
                Message = "Data fetched successfully",
                Status = true,
                Data = subPermission

            };
        }

        public async Task<BaseResponse<IList<BaseResponse<SubPermission>>>> GetSubPermissionsAsync()
        {
            var subPermissions = await _subPermissionRepository.GetAllSubPermissionsAsync();
            var subPermissionsResponse = subPermissions.Select(subPermission => new BaseResponse<SubPermission>
            {
                Message = $"Data fetched successfully",
                Status = true,
                Data = subPermission
            }).ToList();

            return new BaseResponse<IList<BaseResponse<SubPermission>>>
            {
                Message = "Data fetched successfully",
                Status = true,
                Data = subPermissionsResponse
            };
        }
    }
}
