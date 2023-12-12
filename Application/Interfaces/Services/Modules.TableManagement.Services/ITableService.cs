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
    public interface ITableService
    {
        public Task<BaseResponse<List<Table>>> AddTablesPreviewAsync(string userToken, string branchName, CreateTablesRequestModel request);
        public Task<BaseResponse<TableDto>> GetTableByIdAsync(Guid Id);
        public Task<BaseResponse<TableDto>> GetTableByTableIdAsync(string TableId);
        Task<BaseResponse<bool>> DeleteTableAsync(Guid Id);
        public Task<BaseResponse<IList<TableDataResponseDto>>> GetTablesDataByCompanyNameAsync(string businessName);
        Task<BaseResponse<bool>> EnableOrDisableTable(Guid Id, bool isActive);
        public Task<BaseResponse<List<Table>>> SaveTablesAsAsync(string userToken, string branchName);

        // Tables are grouped by branch name
        public Task<BaseResponse<IList<GroupedTablesResponseDto>>> GetGroupedTablesFrorBranchByCompanyNameAsync(string businessName);
    }
}
