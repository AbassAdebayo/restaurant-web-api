using Application.DTOs.TableManagementDtos;
using Application.DTOs;
using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Modules.TableManagement.Services
{
    public interface IGuestService
    {
        public Task<BaseResponse<Guest>> AddNumberOfGuestAsync(string userToken, Guid tableId, AddNumberOfGuestRequestModel request);
        public Task<BaseResponse<GuestDto>> GetNumberOfGuestByIdAsync(Guid guestId);
        public Task<BaseResponse<Guest>> EditNumberOfGuestAsync(string userToken, Guid guestId, EditNumberOfGuestRequestModel request);
        
       
    }
}
