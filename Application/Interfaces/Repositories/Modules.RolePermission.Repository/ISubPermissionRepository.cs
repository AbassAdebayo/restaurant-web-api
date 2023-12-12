using Domain.Domain.Modules.RolePermission.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.RolePermission.Repository
{
    public interface ISubPermissionRepository
    {
        Task<SubPermission> AddSubPermissionAsync(SubPermission subPermission);
        Task<SubPermission> GetSubPermissionByIdAsync(Guid subPermissionId);
        Task<IList<SubPermission>> GetSubPermissionsByIdsAsync(List<Guid> subPermissionIds);
        Task<IList<SubPermission>> GetAllSubPermissionsAsync();
        public Task<SubPermission> GetAsync(Expression<Func<SubPermission, bool>> expression);
    }
}
