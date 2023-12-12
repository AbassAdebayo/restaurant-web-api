using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IIdentityService
    {
        string GetUserIdentity();

        string GenerateToken(User user, IEnumerable<string> roles);
        public IEnumerable<Claim> ValidateToken(string jwtToken);

        JwtSecurityToken GetClaims(string token);

        string GetClaimValue(string type);

        string GenerateSalt();

        public string GetPasswordHash(string password, string salt = null);
        public string GetPincodeHash(string pincode);
        Task<User> FindByNameAsync(string userName);
        Task<User> FindUserAsync(string userName);
        Task<IList<string>> GetRolesAsync(User user);
        bool CheckPasswordAsync(User user, string password);
        public bool VerifyPincode(User user, string enteredPincode);
        public Task<User> GetLoggedInUser();
    }
}
