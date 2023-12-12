using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Table.Repositories
{
    public interface ITabRepository
    {
        public Task<Tab> AddTabAsync(Tab tab);
        public Task<Tab> GetTabByIdAsync(Guid tabId);
        public Task<Tab> GetTabByNameAsync(string tabName);
        public Task<IList<Tab>> GetAllTabsByCompanyAsync(string companyName);
        public Task<IList<Tab>> GetTabsForTableAsync(Guid tableId);
       // public Task<bool> TabExistsByNameAndCompanyName(string tabName, string companyName);
    }
}
