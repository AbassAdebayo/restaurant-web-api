using Application.DTOs;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Domain.Domain.Modules.Users.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace Persistence.Repositories.Modules.Users.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context)
        {
            _context = context;
        }

        public async Task<KYC> AddKYCAsync(KYC kYC)
        {
            await _context.KYCs.AddAsync(kYC);
           await _context.SaveChangesAsync();
            return kYC;
        }

        public async Task<bool> AnyAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.AnyAsync(expression);
        }

        public async Task<bool> AnyAsync(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles.AnyAsync(expression);
        }

        public async Task<bool> KycExistsAsync(Expression<Func<KYC, bool>> expression)
        {
            return await _context.KYCs.AnyAsync(expression);
        }

        public async Task<User> GetAsync(Expression<Func<User, bool>> expression)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<Role> GetAsync(Expression<Func<Role, bool>> expression)
        {
            return await _context.Roles.AsNoTracking().FirstOrDefaultAsync(expression);
        }

        public async Task<User> GetUserAndRoles(Guid userId)
        {
            var user = await _context.Users.Include(r => r.UserRoles)
                .ThenInclude(r => r.Role).FirstOrDefaultAsync(u => u.Id == userId);
            return user;
        }

        public async Task<User> GetUserByEmail(string email)
        {

            return await _context.Users
                .SingleOrDefaultAsync(user => user.Email == email);




        }

        public async Task<IList<UsersInRoleResponseModel>> GetUsersInRoleAsync(string roleName)
        {
            var userRoles = await _context.UserRoles.Include(u => u.User)
                .Include(u => u.Role)
                .AsNoTracking()
                .Where(u => u.Role.RoleName == roleName)
                .Select(x => new UsersInRoleResponseModel
                {
                    Id = x.Id,
                    Email = x.User.Email,
                    Roles = x.User.UserRoles.Select(u => new RoleDto
                    {
                        RoleId = u.Role.Id,
                        Name = u.Role.RoleName,
                    }).ToList(),
                    Status = true,
                    Message = "Success"
                })
                .ToListAsync();

            return userRoles;


        }

        public async Task<User> UpdatePincodeVerifiedStatus(Guid userId, bool isVerified)
        {
            var getUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            getUser.PincodeVerified = isVerified;
            getUser.LastModifiedOn = DateTime.UtcNow;
            _context.Users.Update(getUser);
            await _context.SaveChangesAsync();
            return getUser;
        }

        public async Task<User> UpdateUserAsync(User user)
        {
             _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateUserToVerifiedAsync(Guid userId, User user)
        {
            var getUser = _context.Users.FirstOrDefault(u => u.Id == userId);
            getUser.EmailConfirmed = true;
            getUser.LastModifiedOn = DateTime.UtcNow;
            _context.Users.Update(getUser);
            await _context.SaveChangesAsync();
            return getUser;

        }

        public async Task<User> GetUserByIdAsync(Guid userId)
        {
             var user = await _context.Users.Where(u => u.Id == userId)
                .Include(userroles => userroles.UserRoles)
                .ThenInclude(role => role.Role)
                .SingleOrDefaultAsync();
            return user;
        }

        public async Task<bool> DeleteUser(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
