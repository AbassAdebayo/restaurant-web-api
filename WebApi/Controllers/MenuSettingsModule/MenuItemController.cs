using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/menuitem")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpPost("add-menuitem/{menuGroupId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddMenuItem([FromRoute] string token, [FromRoute] Guid menuGroupId, [FromBody] CreateMenuItemRequestModel model)
        {
            var response = await _menuItemService.AddMenuItemAsync(token, menuGroupId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpDelete("delete-menuitem/{menuItemId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> DeleteMenuItem([FromRoute] Guid menuItemId)
        //{
        //    var response = await _menuItemService.DeleteMenuItemAsync(menuItemId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpPut("edit-menuitem/{menuItemId:guid}/{token}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> EditMenuItem([FromRoute] Guid menuItemId, [FromRoute] string token, [FromBody] EditMenuItemRequestModel model)
        //{
        //    var response = await _menuItemService.EditMenuItemAsync(token, menuItemId, model);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        [HttpGet("get-all-menuitems-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllMenuItems([FromQuery] string businessName)
        {
            var response = await _menuItemService.GetAllMenuItemsByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpGet("get-menuitem-by-id/{menuItemId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetMenuGroupById([FromRoute] Guid menuItemId)
        //{
        //    var response = await _menuItemService.GetMenuItemByIdAsync(menuItemId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

    }
}
