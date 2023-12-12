using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.MenuSettings.Services
{
    public interface IPriceListService
    {
        
        //public Task<BaseResponse<IList<PriceListEntry>>> GetPriceListAsync(string businessName);
        public Task<BaseResponse<IList<PriceListResponseDto>>> GetPriceListAsync(string businessName);
        public Task<BaseResponse<PriceListEntry>> EditPriceListItemAsync(string userToken, Guid menuItemId, EditPriceListItemRequestModel request);
    }
}
