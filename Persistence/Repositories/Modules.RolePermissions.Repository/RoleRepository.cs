using Application.DTOs;
using Application.Exceptions;
using Application.Interfaces.Modules.RolePermission.Repository;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.RolePermission.Repository
{
    public class RoleRepository : IRoleRepository
    {
        private readonly ApplicationContext _context;

        public RoleRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Role> AddRoleAsync(Role role)
        {
            await _context.Roles.AddAsync(role);
            await _context.SaveChangesAsync();
            return role;
        }

        public async Task AssignRoleToUserAsync(Guid userId, Guid roleId)
        {
            var userRole = new UserRole
            {
                UserId = userId,
                RoleId = roleId
            };

            await _context.UserRoles.AddAsync(userRole);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> DeleteRole(Role role)
        {
            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<IList<Role>> GetAllRolesBySuperAdminAsync(string businessName)
        {
            var roles = await _context.Roles
                .Include(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
                 .ThenInclude(p => p.SubPermissions)
                 .Where(sa => sa.CreatedBy == businessName && sa.RoleName != "SuperAdmin")
                 .AsNoTracking()
                 .ToListAsync();


            return roles;

            
        }

        public async Task<Role> GetRoleByIdAsync(Guid roleId)
        {
            var role =  await _context.Roles.Include(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
                 .ThenInclude(p => p.SubPermissions)
                 .Where(r => r.Id == roleId)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();
            return role;
        }

        public async Task<Role> GetRoleByName(string roleName)
        {
            var role = await _context.Roles
                 .Include(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
                 .ThenInclude(p => p.SubPermissions)
                 .Where(r => r.RoleName == roleName)
                 .AsNoTracking()
                 .FirstOrDefaultAsync();
                return role;
        }

        public async Task<List<Role>> GetRolesByUserAsync(Guid userId)
        {
             var roleIds = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

                var roles = await _context.Roles
                .Include(r => r.RolePermissions)
                 .ThenInclude(rp => rp.Permission)
                 .ThenInclude(p => p.SubPermissions)
                    .Where(r => roleIds.Contains(r.Id))
                    .ToListAsync();

            return roles;
        }

        public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
        {
            var userRole = await _context.UserRoles
            .SingleOrDefaultAsync(ur => ur.UserId == userId && ur.RoleId == roleId);

            if (userRole != null)
            {
                _context.UserRoles.Remove(userRole);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> RoleExistsBySuperAdmin(string roleName, string buisnessName)
        {
            return await _context.Roles.AnyAsync(r => r.RoleName == roleName && r.CreatedBy == buisnessName);
        }

        public async Task<Role> UpdateRoleAsync(Role role)
        {
            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
            return role;
        }
    }
}
