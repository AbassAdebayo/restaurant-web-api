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
    public class SubPermissionRepository : ISubPermissionRepository
    {
        private readonly ApplicationContext _context;

        public SubPermissionRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<SubPermission> AddSubPermissionAsync(SubPermission subPermission)
        {
            await _context.SubPermissions.AddAsync(subPermission);
            await _context.SaveChangesAsync();
            return subPermission;
        }

        public async Task<IList<SubPermission>> GetAllSubPermissionsAsync()
        {
            var subPermissions = await _context.SubPermissions
                 .AsNoTracking()
                 .ToListAsync();
            return subPermissions;
        }

        public async Task<SubPermission> GetAsync(Expression<Func<SubPermission, bool>> expression)
        {
            return await _context.SubPermissions.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<SubPermission> GetSubPermissionByIdAsync(Guid subPermissionId)
        {
            var subPermission = await _context.SubPermissions.Where(sub => sub.Id == subPermissionId)
                 .AsNoTracking()
                 .SingleOrDefaultAsync();
            return subPermission;
        }

        public async Task<IList<SubPermission>> GetSubPermissionsByIdsAsync(List<Guid> subPermissionIds)
        {
            return await _context.SubPermissions
            .Where(sbp => subPermissionIds.Contains(sbp.Id))
            .Include(sbp => sbp.Permission)
            .ToListAsync();
        }
    }
}
