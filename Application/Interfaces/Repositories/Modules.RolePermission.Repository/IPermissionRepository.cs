using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.RolePermission.Repository
{
    public interface IPermissionRepository
    {

        public Task<Permission> GetAsync(Expression<Func<Permission, bool>> expression);
        public Task<Permission> GetPermissionByIdAsync(Guid permissionId);
        public Task<IList<Permission>> GetPermissionsByIdsAsync(List<Guid> permissionIds);
        public Task DeleteAsync(Permission entity);
        public Task<Permission> AddAsync(Permission entity);
        public Task<IList<Permission>> GetAllPermissionsAsync();


    }
}
