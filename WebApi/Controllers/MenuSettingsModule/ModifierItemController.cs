using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/modifieritem")]
    public class ModifierItemController : ControllerBase
    {
        private readonly IModifierItemService _modifierItemService;

        public ModifierItemController(IModifierItemService modifierItemService)
        {
            _modifierItemService = modifierItemService;
        }

        [HttpPost("add-modifier-item/{modifierGroupId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddModifierItem([FromRoute] string token, [FromRoute] Guid modifierGroupId, [FromBody] CreateModifierItemRequestModel model)
        {
            var response = await _modifierItemService.AddModifierItemAsync(token, modifierGroupId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpDelete("delete-modifier-item/{modifierItemId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> DeleteModifierItem([FromRoute] Guid modifierItemId)
        //{
        //    var response = await _modifierItemService.DeleteModifierItemAsync(modifierItemId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        [HttpPut("edit-modifier-item/{modifierItemId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EditModifierItem([FromRoute] Guid modifierItemId, [FromRoute] string token, [FromBody] EditModifierItemRequestModel model)
        {
            var response = await _modifierItemService.EditModifierItemAsync(token, modifierItemId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-modifieritems-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllModifierItems([FromQuery] string businessName)
        {
            var response = await _modifierItemService.GetAllModifierItemsByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpGet("get-modifier-item-by-id/{modifierItemId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetMenuGroupById([FromRoute] Guid modifierItemId)
        //{
        //    var response = await _modifierItemService.GetModifierItemByIdAsync(modifierItemId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}
    }
}
