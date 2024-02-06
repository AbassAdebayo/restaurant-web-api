using Application.Interfaces.Modules.RolePermission.Repository;
using Domain.Domain.Modules.RolePermission.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.RolePermissions.Repository
{
    public class RolePermissionsRepository : IRolePermissionsRepository
    {
        private readonly ApplicationContext _context;

        public RolePermissionsRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<Domain.Domain.Modules.RolePermission.Entities.RolePermission> AddRolePermissionAsync(Domain.Domain.Modules.RolePermission.Entities.RolePermission rolePermission)
        {
            await _context.RolePermissions.AddAsync(rolePermission);
            await _context.SaveChangesAsync();
            return rolePermission;
        }

        public async Task<IEnumerable<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> AddRolePermissionsAsync(IEnumerable<Domain.Domain.Modules.RolePermission.Entities.RolePermission> rolePermissions)
        {
            await _context.RolePermissions.AddRangeAsync(rolePermissions);
            await _context.SaveChangesAsync();
            return rolePermissions;
        }

        public async Task<IList<Domain.Domain.Modules.RolePermission.Entities.RolePermission>> GetAllRolePermissionsAsync()
        {
            var rolePermissions = await _context.RolePermissions.Include(rp => rp.Permission)
                .ThenInclude(pm => pm.SubPermissions)
                  .AsNoTracking()
                  .ToListAsync();
            return rolePermissions;
        }

        public async Task<List<Permission>> GetPermissionsByRoleAsync(Guid roleId)
        {
             var permissionIds = await _context.RolePermissions
            .Where(rp => rp.RoleId == roleId)
            .Select(rp => rp.PermissionId)
            .ToListAsync();

                var permissions = await _context.Permissions
                    .Where(p => permissionIds.Contains(p.Id))
                    .Include(pm => pm.SubPermissions)
                    .ToListAsync();

                return permissions;
        }

        public async Task<Domain.Domain.Modules.RolePermission.Entities.RolePermission> GetRolePermissionByIdAsync(Guid rolePermissionId)
        {
            var rolePermission = await _context.RolePermissions.Include(rp => rp.Permission)
                .ThenInclude(pm => pm.SubPermissions)
                .Where(role => role.Id == rolePermissionId)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();
            return rolePermission;

        }

        public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId)
        {
            var rolePermission = await _context.RolePermissions
            .SingleOrDefaultAsync(rp => rp.RoleId == roleId && rp.PermissionId == permissionId);

            if (rolePermission != null)
            {
                _context.RolePermissions.Remove(rolePermission);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> UserHasPermissionAndSubPermissionAsync(Guid userId, List<string> permissionNames, List<string> subPermissionNames)
        {
            var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

            var hasPermissions = await _context.RolePermissions
                .Where(rp => userRoleIds.Contains(rp.RoleId))
                .AnyAsync(rp =>
                    permissionNames.Contains(rp.Permission.Name));
                    

            return hasPermissions;
        }

        public async Task<bool> UserHasPermissionAsync(Guid userId, string permissionName)
        {
            var userRoleIds = await _context.UserRoles
            .Where(ur => ur.UserId == userId)
            .Select(ur => ur.RoleId)
            .ToListAsync();

            var rolePermissions = await _context.RolePermissions
                .Where(rp => userRoleIds.Contains(rp.RoleId))
                .Include(rp => rp.Permission)
                .ToListAsync();

            return rolePermissions.Any(rp =>
                rp.Permission.Name == permissionName ||
                rp.Permission.SubPermissions.Any(sp => sp.Name == permissionName));
        }
    }
}
