using Application.DTOs;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Users.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        public Task<IList<UsersInRoleResponseModel>> GetUsersInRoleAsync(string roleName);
        public Task<User> UpdateUserAsync(User user);
        public Task<bool> AnyAsync(Expression<Func<User, bool>> expression);
        public Task<bool> KycExistsAsync(Expression<Func<KYC, bool>> expression);
        public Task<Role> GetAsync(Expression<Func<Role, bool>> expression);
        public Task<User> GetUserByIdAsync(Guid userId);
        public Task<User> GetUserAndRoles(Guid userId);
        public Task<User> GetUserByEmail(string email);
        Task<User> UpdateUserToVerifiedAsync(Guid userId, User user);
        Task<User> UpdatePincodeVerifiedStatus(Guid userId, bool isVerified);
        public Task<KYC> AddKYCAsync(KYC kYC);
        Task<bool> DeleteUser(User user);
    }
}
