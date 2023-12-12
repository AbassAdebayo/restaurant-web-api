using Application.DTOs;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Modules.Employee.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<User> GetEmployeeByIdAsync(Guid employeeUserId);
        Task<bool> DeleteEmployee(User employeeUser);
        public Task<User> UpdateEmployeeAsync(User employeeUser);
        public Task<IList<User>> GetAllEmployeesBySuperAdminAsync(string buisnessName);
        Task<User> GetEmployeeByEmailAndSuperAdmin(string email, string businessName);

    }
}
