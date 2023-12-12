using Application.Interfaces;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Repositories.Modules.Users.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;

namespace Persistence.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IUserRepository _userRepository;

        public IdentityService(IHttpContextAccessor httpContextAccessor, IConfiguration configuration, IPasswordHasher<User> passwordHasher,
            IUserRepository userRepository)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _configuration = configuration ?? throw new ArgumentException(nameof(configuration));
            _passwordHasher = passwordHasher ?? throw new ArgumentException(nameof(passwordHasher));
            _userRepository = userRepository;
        }

        public bool CheckPasswordAsync(User user, string password)
        {
            var hashPassword = HashPasswordAsync(password);
            if (user.PasswordHash == hashPassword)
            {
                return true;
            }
            return false;
        }

        public string GetUniqueKey(int size)
        {
            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            using var crypto = RandomNumberGenerator.Create();
            byte[] data = new byte[size];
            crypto.GetBytes(data);
            /*using (var crypto = new RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }*/
            StringBuilder result = new StringBuilder(size);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }
            return result.ToString();
        }

        public Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public async Task<User> FindUserAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new ArgumentNullException(nameof(email));
            }
            //var user = await _gateway.GetUserAsync(userName);
            var user = await _userRepository.GetAsync(e => e.Email == email);
            if (user == null)
            {
                return null;
            }
            //return user;
            return new User
            {

                Email = user.Email,
                BusinessName = user.BusinessName

            };
        }

        public string GenerateSalt()
        {
            using var crypto = RandomNumberGenerator.Create();
            byte[] buffer = new byte[10];
            crypto.GetBytes(buffer);
            return Convert.ToBase64String(buffer);
        }

        public string GenerateToken(User user, IEnumerable<string> roles)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JwtTokenSettings:TokenKey").Value));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512);
            IList<Claim> claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }
            var token = new JwtSecurityToken("", "", claims,
                DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetSection("JwtTokenSettings:TokenExpiryPeriod").Value)),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public JwtSecurityToken GetClaims(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                if (token.StartsWith("B"))
                {
                    token = token.Split(" ")[1];
                }
                var handler = new JwtSecurityTokenHandler();

                var decodedToken = handler.ReadToken(token) as JwtSecurityToken;

                return decodedToken;
            }
            return null;
        }

        public string GetClaimValue(string type)
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(type).Value;
        }

        public string GetPasswordHash(string password, string salt = null)
        {
            if (string.IsNullOrEmpty(salt))
            {
                return _passwordHasher.HashPassword(new User(), password);
            }
            return _passwordHasher.HashPassword(new User(), $"{password}{salt}");
        }

        public async Task<IList<string>> GetRolesAsync(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }
            var roles = await _userRepository.GetUserAndRoles(user.Id);

            return roles.UserRoles.Select(role => role.Role.RoleName).ToList();
        }

        public string GetUserIdentity()
        {
            return _httpContextAccessor.HttpContext?.User?.FindFirst(JwtRegisteredClaimNames.UniqueName)?.Value;
        }

        private string HashPasswordAsync(string password)
        {
            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(password);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash.ToLower();
            }
        }

        public string GetPincodeHash(string pincode)
        {
            return HashPincode(pincode);
        }

        public bool VerifyPincode(User user, string enteredPincode)
        {

            // Hash the entered PIN code for comparison
            string enteredHashedPincode = HashPincode(enteredPincode);

            // Compare the entered hashed PIN code with the stored hashed PIN code
            return string.Equals(enteredHashedPincode, user.PinCode, StringComparison.OrdinalIgnoreCase);

        }

        private string HashPincode(string pincode)
        {

            using (var md5Hash = MD5.Create())
            {
                var sourceBytes = Encoding.UTF8.GetBytes(pincode);
                var hashBytes = md5Hash.ComputeHash(sourceBytes);
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);
                return hash.ToLower();
            }
        }

        public IEnumerable<Claim> ValidateToken(string jwtToken)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetSection("JwtTokenSettings:TokenKey").Value);

            try
            {
                // Set the validation parameters for the token
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key.ToArray()),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    //ValidIssuer = _configuration.GetSection("JwtTokenSettings:TokenIssuer").Value,
                    ClockSkew = TimeSpan.Zero
                };

                // Validate the token and extract the claims
                var claimsPrincipal = tokenHandler.ValidateToken(jwtToken, validationParameters, out var validatedToken);
                var claims = ((JwtSecurityToken)validatedToken).Claims;

                // Return the claims
                return claims;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<User> GetLoggedInUser()
        {
            // Get the current HttpContext
            var httpContext = _httpContextAccessor.HttpContext;

            // Check if a user is authenticated
            if (httpContext.User.Identity.IsAuthenticated)
            {
                // Retrieve the user's unique identifier (e.g., user ID) from claims
                var email = httpContext.User.FindFirst(ClaimTypes.Email)?.Value;
                var user = await _userRepository.GetUserByEmail(email);

                return user;
                
           
                
            }

            // If no user is authenticated, return null
            throw new BadHttpRequestException("Unable to get logged in user");
        }

        
    }
}
