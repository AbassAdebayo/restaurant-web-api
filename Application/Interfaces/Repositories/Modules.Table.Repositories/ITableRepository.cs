using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Tables;
using Domain.Wrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Table.Repositories
{
    public interface ITableRepository
    {
        public Task<Domain.Domain.Modules.Tables.Table> SaveTableAs(Domain.Domain.Modules.Tables.Table tables);
        public Task<Domain.Domain.Modules.Tables.Table> GetTableByIdAsync(Guid Id);
        public Task<Domain.Domain.Modules.Tables.Table> GetTableByTabAsync(Tab tab);
        public Task<Domain.Domain.Modules.Tables.Table> GetTableByTableIdAsync(string tableId);
        Task<bool> DeleteTable(Domain.Domain.Modules.Tables.Table table);
        public Task<Domain.Domain.Modules.Tables.Table> EditTableAsync(Domain.Domain.Modules.Tables.Table table);
        //public Task<PaginatedList<Domain.Domain.Modules.Tables.Table>> GetPaginatedTablesByCompanyNameAsync(string companyName, PaginationFilter filter);
        public Task<IList<Domain.Domain.Modules.Tables.Table>> GetAllTablesByCompanyNameAsync(string companyName);
        public Task<IList<Domain.Domain.Modules.Tables.Table>> GetAllTablesByBranchAndCompanyNameAsync(string companyName, string branchName);

        public Task<IList<Domain.Domain.Modules.Tables.Table>> GetTablesDataByCompanyNameAsync(string companyName);
        public Task<IList<Domain.Domain.Modules.Tables.Table>> GetAllOpenedTablesByCompanyNameAsync(string companyName);
        public Task<IList<Domain.Domain.Modules.Tables.Table>> GetAllClosedTablesByCompanyNameAsync(string companyName);
    }
}
