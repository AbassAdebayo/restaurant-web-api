using Application.DTOs.RequestModel.User;
using Application.Interfaces.Modules.Users.IService;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Application.DTOs;
using Microsoft.AspNetCore.Authorization;
using System.Net.Http.Headers;
using Application.Interfaces;
using Application.DTOs.RequestModel;
using WebApi.Filters;

namespace WebApi.Controllers.AuthModule
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;
        private readonly IIdentityService _identityService;

        public UserController(IConfiguration configuration, IUserService userService, ILogger<UserController> logger, IIdentityService identityService)
        {
            _configuration = configuration;
            _userService = userService;
            _logger = logger;
            _identityService = identityService;
        }

        [HttpPost("createadmin")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(CreateUserAdmin))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> Create([FromBody] CreateUserAdmin model)
        {
            var response = await _userService.AddUserAsync(model);
            return Ok(response);
        }



        [HttpPost("create-kyc")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> CreateKYC([FromBody] KYCRequestModel model)
        {
            var response = await _userService.AddKycAsync(model);
            return Ok(response);
        }

        [HttpGet("kyc-exist")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> KYCExists([FromQuery] Guid superAdminId)
        {
            var response = await _userService.KycExistsAsync(superAdminId);
            return Ok(response);
        }

        [HttpGet("GetUserByEmail")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetUser(string email)
        {
            var response = await _userService.GetUserAsync(email);

            return Ok(response);
        }

        [HttpGet("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            var response = await _userService.GetUserById(userId);

            return Ok(response);
        }

        [HttpDelete("{userId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var response = await _userService.DeleteUserAsync(userId);

            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
