using Application.DTOs;
using Application.DTOs.TableManagementDtos;
using Application.Implementations.Modules.TableManagement.Services;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.TableManagementModule
{
    [Route("api/guest")]
    public class GuestController : ControllerBase
    {
        private readonly IGuestService _guestService;

        public GuestController(IGuestService guestService)
        {
            _guestService = guestService;
        }

        [HttpPost("add-no-of-guests/{token}/{tableId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddNumberOfGuests([FromRoute] string token, [FromRoute] Guid tableId, [FromBody] AddNumberOfGuestRequestModel model)
        {
            var response = await _guestService.AddNumberOfGuestAsync(token, tableId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("edit-number-of-guests/{Id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EditNumberOfGuests([FromRoute] string userToken, [FromRoute] Guid Id, [FromBody] EditNumberOfGuestRequestModel request)
        {
            var response = await _guestService.EditNumberOfGuestAsync(userToken, Id, request);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
