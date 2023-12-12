using Application.DTOs;
using Application.DTOs.TableManagementDtos;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Domain.Domain.Modules.Tables;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.Modules.Table.Repositories;
using Domain.Domain.Modules.MenuSettings;
using Application.Exceptions;

namespace Application.Implementations.Modules.TableManagement.Services
{
    public class GuestService : IGuestService
    {
        private readonly IGuestRepository _guestRepository;
        private readonly ITableRepository _tableRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<TableService> _logger;

        public GuestService(IGuestRepository guestRepository, ITableRepository tableRepository, IIdentityService identityService, IUserRepository userRepository, IRolePermissionsRepository rolePermissionsRepository, ILogger<TableService> logger)
        {
            _guestRepository = guestRepository;
            _tableRepository = tableRepository;
            _identityService = identityService;
            _userRepository = userRepository;
            _rolePermissionsRepository = rolePermissionsRepository;
            _logger = logger;
        }

        public async Task<BaseResponse<Guest>> AddNumberOfGuestAsync(string userToken, Guid tableId, AddNumberOfGuestRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);

            // get user email from claims
            var email = claims.SingleOrDefault(c => c.Type == "email");

            // get user by email retrieved from claims
            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user == null)
            {
                _logger.LogError("User not authenticated");
                return new BaseResponse<Guest>
                {
                    Message = "User not authenticated!",
                    Status = false
                };
            }

            // check if user is verified
            if (!user.EmailConfirmed) throw new BadRequestException("User unverified");

            if (user.UserType == Domain.Entities.Enums.UserType.Employee)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Table Ordering");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<Guest>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            // get selected table by Id
            var getTableNumber = await _tableRepository.GetTableByIdAsync(tableId);

            if (getTableNumber is null)
            {
                _logger.LogError("The Table number accessed couldn't be fetched!");
                return new BaseResponse<Guest>
                {
                    Message = "The Table number accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Guest>              {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            if (request.NumberOfGuest <= 0)
            {
                _logger.LogError("Number of guest must be greater than zero.");
                return new BaseResponse<Guest>
                {
                    Message = "number of guest must be greater than zero.",
                    Status = false
                };
            }


            var newGuest = new Guest
            {
                NumberOfGuest = request.NumberOfGuest,
                TableId = getTableNumber.Id,
                CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}",
                CreatedOn = DateTime.UtcNow,
            };

            var addnumberOfGuest = await _guestRepository.AddNumberOfGuestAsync(newGuest);
            if(addnumberOfGuest is null)
            {
                _logger.LogError("Guest(s) couldn't be added.");
                return new BaseResponse<Guest>
                {
                    Message = "Guest(s) couldn't be added.",
                    Status = false
                };
            }

            _logger.LogError($"{request.NumberOfGuest} guest(s) added successfully.");
            return new BaseResponse<Guest>
            {
                Message = $"{request.NumberOfGuest} guest(s) added successfully.",
                Status = true,
                Data = addnumberOfGuest
            };
        }

        public async Task<BaseResponse<Guest>> EditNumberOfGuestAsync(string userToken, Guid guestId, EditNumberOfGuestRequestModel request)
        {
            //Get logged in user
            var claims = _identityService.ValidateToken(userToken);
            var email = claims.SingleOrDefault(c => c.Type == "email");

            var user = await _userRepository.GetUserByEmail(email.Value);


            if (user.UserType != Domain.Entities.Enums.UserType.SuperAdmin)
            {
                var userHasPermission = await _rolePermissionsRepository.UserHasPermissionAsync(user.Id, "Menu Settings");

                if (!userHasPermission)
                {
                    _logger.LogError("Unauthorized!");
                    return new BaseResponse<Guest>
                    {
                        Message = "Unauthorized!",
                        Status = false
                    };
                }
            }

            var existingNumberOfGuest = await _guestRepository.GetNumberOfGuestByIdAsync(guestId);
            if(existingNumberOfGuest is null)
            {
                _logger.LogError("Number of guest accessed couldn't be fetched!");
                return new BaseResponse<Guest>
                {
                    Message = "Number of guest accessed couldn't be fetched!",
                    Status = false
                };
            }

            if (request is null)
            {
                _logger.LogError("Fields cannot be empty!");
                return new BaseResponse<Guest>
                {
                    Message = "Fields cannot be empty!",
                    Status = false
                };
            }

            if (request.NumberOfGuest <= 0)
            {
                _logger.LogError("Number of guest must be greater than zero.");
                return new BaseResponse<Guest>
                {
                    Message = "number of guest must be greater than zero.",
                    Status = false
                };
            }
            existingNumberOfGuest.NumberOfGuest = request.NumberOfGuest;
            existingNumberOfGuest.CompanyName = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : user.CreatedBy;
            existingNumberOfGuest.LastModifiedOn = DateTime.UtcNow;
            existingNumberOfGuest.CreatedBy = user.UserType == Domain.Entities.Enums.UserType.SuperAdmin ? user.BusinessName : $"{user.Name} {user.LastName}";

            var updateNumberOfGuest = await _guestRepository.EditNumberOfGuestAsync(existingNumberOfGuest);
            if (updateNumberOfGuest is null)
            {
                _logger.LogError("Number of guest update unsuccessful");
                return new BaseResponse<Guest>
                {
                    Message = "Number of guest update unsuccessful",
                    Status = false
                };
            }

            _logger.LogError("Number of guest update succeeded");
            return new BaseResponse<Guest>
            {
                Message = "Number of guest update succeeded",
                Status = true
            };
        }

        public Task<BaseResponse<GuestDto>> GetNumberOfGuestByIdAsync(Guid guestId)
        {
            throw new NotImplementedException();
        }
    }
}
