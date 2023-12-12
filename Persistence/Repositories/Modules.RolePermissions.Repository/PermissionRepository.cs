using Application.Interfaces.Repositories.Modules.RolePermission.Repository;
using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.RolePermission.Repository
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly ApplicationContext _context;

        public PermissionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Permission> AddAsync(Permission entity)
        {
            await _context.Permissions.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> AnyAsync(Expression<Func<Permission, bool>> expression)
        {
            return await _context.Permissions.AnyAsync(expression);
        }

        public async Task DeleteAsync(Permission entity)
        {
            _context.Permissions.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<IList<Permission>> GetAllPermissionsAsync()
        {

            var unsortedPermissions =  await _context.Permissions.Include(p => p.SubPermissions)
             .AsNoTracking()
            .ToListAsync();

            var customOrder = new[] { "Menu Settings", "Cash Register" , "Till", "Tickets", "Table Ordering", "Kitchen Display System" };
            var sortedPermissions = customOrder
            .Select(permissionName => unsortedPermissions.FirstOrDefault(p => p.Name == permissionName))
            .Where(permission => permission != null)
            .ToList();

            return sortedPermissions;
        }

        public async Task<Permission> GetAsync(Expression<Func<Permission, bool>> expression)
        {
            return await _context.Permissions.Include(pm => pm.SubPermissions)
                .AsNoTracking()
                .FirstOrDefaultAsync(expression);
        }

        

        public async Task<Permission> GetPermissionByIdAsync(Guid permissionId)
        {
            var permission = await _context.Permissions.Include(p => p.SubPermissions)
                .Where(pm =>  pm.Id == permissionId)
                .SingleOrDefaultAsync();
            return permission;
        }

        public async Task<IList<Permission>> GetPermissionsByIdsAsync(List<Guid> permissionIds)
        {
             return await _context.Permissions
            .Where(p => permissionIds.Contains(p.Id))
            .Include(p => p.SubPermissions)
            .ToListAsync();
        }
    }
}
