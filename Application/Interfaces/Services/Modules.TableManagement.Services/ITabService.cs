using Application.DTOs;
using Application.DTOs.TableManagementDtos;
using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Modules.TableManagement.Services
{
    public interface ITabService
    {
        public Task<BaseResponse<Tab>> AddTabAsync(string userToken, Guid tableId, AddTabRequestModel request);
        public Task<BaseResponse<TabDto>> GetTabByIdAsync(Guid tabId);
        //public Task<BaseResponse<TabDto>> GetTabByNameAsync(string tabName);
        public Task<BaseResponse<IList<TabDto>>> GetAllTabsByCompanyAsync(string companyName);
        public Task<BaseResponse<IList<TabDto>>> GetTabsForTableAsync(Guid tableId);
    }
}
