using Application.DTOs;
using Application.Interfaces;
using Application.Interfaces.Modules.Users.IService;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Domain.Entities.Enums;
using WebApi.Filters;
using Application.DTOs.RequestModel.User;
using Application.Interfaces.Repositories.Modules.Users.IRepository;

namespace WebApi.Controllers.AuthModule
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<AuthController> _logger;
        private readonly UserManager<User> _userManager;
        private readonly IIdentityService _identityService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger, UserManager<User> userManager, IIdentityService identityService,
            IUserService userService, IUserRepository userRepository)
        {
            _configuration = configuration;
            _logger = logger;
            _userManager = userManager;
            _identityService = identityService;
            _userService = userService;
            _userRepository = userRepository;
        }

        [HttpPost("UserLogin")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(LoginResponseModel))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> Login([FromBody] LoginRequestModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            List<string> adminRole = new List<string> { "SuperAdmin" };

            if (user != null)
            {

                var isValidPassword = await _userManager.CheckPasswordAsync(user, model.Password);
                if (!isValidPassword)
                {
                    var failedResponse = new BaseResponse
                    {
                        Message = "Invalid Credential",
                        Status = false
                    };
                    return BadRequest(failedResponse);
                }

                //if (!user.EmailConfirmed)
                //{
                //    var unverifiedResponse = new BaseResponse
                //    {
                //        Message = "This user has not been verified!",
                //        Status = false
                //    };
                //    return BadRequest(unverifiedResponse);
                //}
                
                var roles = await _userManager.GetRolesAsync(user);
                var token = _identityService.GenerateToken(user, roles);
                var expiry = DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetValue<string>("JwtTokenSettings:TokenExpiryPeriod")));
                var tokenResponse = new LoginResponseModel
                {
                    Message = "Login Successful",
                    Status = true,
                    Data = new LoginResponseData
                    {
                        Roles = user.UserType == UserType.SuperAdmin ? adminRole : roles,
                        Email = user.Email,
                        BusinessName = user.UserType == UserType.SuperAdmin ? user.BusinessName : user.CreatedBy,
                        Token = token,
                        UserId = user.Id


                    }
                };

                Response.Headers.Add("Token", token);
                Response.Headers.Add("TokenExpiry", expiry.ToUnixTimeMilliseconds().ToString());
                Response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
                return Ok(tokenResponse);
            }
            var response = new BaseResponse
            {
                Message = "Invalid Credential",
                Status = false
            };
            return BadRequest(response);
        }

        [HttpPost("token")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Token([FromBody] TokenRequestModel model)
        {
            var response = await _userService.CreateToken(model);
            if (response is null) return BadRequest(response);

            // set epiry to token and add to header
            var token = response.Token;
            var expiry = DateTimeOffset.UtcNow.AddMinutes(Convert.ToInt32(_configuration.GetValue<string>("JwtTokenSettings:TokenExpiryPeriod")));
            Response.Headers.Add("Token", token);
            Response.Headers.Add("TokenExpiry", expiry.ToUnixTimeMilliseconds().ToString());
            Response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            return Ok(response);
        }

        [HttpGet("verify-user/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> VerifyUser([FromRoute] string token)
        {
            var response = await _userService.VerifyUser(token);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("verify/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Verify([FromRoute] string token)
        {
            var response = await _userService.VerifyToken(token);
            return response.Status ? Ok(response) : BadRequest(response);
        }


        [HttpPost("verify-pincode/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> VerifyPincode([FromRoute] string token, [FromBody] string pincode)
        {
            var response = await _userService.VerifyPinCode(token, pincode);
            return !response.Status ? Ok(response) : BadRequest(response);

        }

        [HttpPost("password-and-pincode-reset/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ResetPasswordAndPincodeReset([FromRoute] string token, [FromBody] ResetPasswordAndPincodeRequest request)
        {
            var response = await _userService.ResetPasswordAndPincode(token, request);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("forgotpassword")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequest request)
        {
            var response = await _userService.ForgotPassword(request);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("passwordreset/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> PasswordReset([FromRoute] string token, [FromBody] PasswordResetRequestModel request)
        {
            var response = await _userService.PasswordReset(token, request);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("changepincode")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePincode request)
        {
            var response = await _userService.ChangePincode(request);
            return response.Status ? Ok(response) : BadRequest(response);
        }


        [HttpPost("logout")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> Logout()
        {
            var user = await _identityService.GetLoggedInUser();
            if (user is null)
            {

                await _userRepository.UpdatePincodeVerifiedStatus(user.Id, false);

                return Ok(new { message = "Logout successful" });

            }

            return BadRequest(new { message = "User not authenticated" });
        }


    }
}
