using Application.DTOs;
using Application.Interfaces.Modules.Employee.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Employees.Repository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationContext _context;

        public EmployeeRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<bool> DeleteEmployee(User employeeUser)
        {
            _context.Users.Remove(employeeUser);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<User> GetEmployeeByEmailAndSuperAdmin(string email, string businessName)
        {
            return await _context.Users.Where(u => u.Email == email && u.CreatedBy == businessName)
                .FirstOrDefaultAsync();
        }

        public async Task<IList<User>> GetAllEmployeesBySuperAdminAsync(string businessName)
        {
            var employeeUsers = await _context.Users
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .ThenInclude(rp => rp.RolePermissions)
                .ThenInclude(p => p.Permission)
                .ThenInclude(sb=> sb.SubPermissions)
                .Where(u => u.UserType == Domain.Entities.Enums.UserType.Employee && u.CreatedBy == businessName)
                .AsNoTracking()
                .ToListAsync();

            return employeeUsers;
        }

        public async Task<User> GetEmployeeByIdAsync(Guid employeeUserId)
        {
            var employeeUser = await _context.Users.Where(u => u.Id == employeeUserId && u.UserType == Domain.Entities.Enums.UserType.Employee)
                .Include(u => u.UserRoles)
                .ThenInclude(ur => ur.Role)
                .SingleOrDefaultAsync();

            return employeeUser;
        }

        public async Task<User> UpdateEmployeeAsync(User employeeUser)
        {
            _context.Users.Update(employeeUser);
            await _context.SaveChangesAsync();
            return employeeUser;
            
        }
    }
}
