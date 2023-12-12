using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/publish")]
    public class PublishController : ControllerBase
    {
        private readonly IMenuGroupService _menuGroupService;
        private readonly IMenuItemService _menuItemService;
        private readonly IMenuService _menuService;
        private readonly IModifierGroupService _modifierGroupService;
        private readonly IModifierItemService _modifierItemService;
        private readonly ILogger<PublishController> _logger;

        public PublishController(IMenuGroupService menuGroupService, IMenuItemService menuItemService, IMenuService menuService, IModifierGroupService modifierGroupService, IModifierItemService modifierItemService, ILogger<PublishController> logger)
        {
            _menuGroupService = menuGroupService;
            _menuItemService = menuItemService;
            _menuService = menuService;
            _modifierGroupService = modifierGroupService;
            _modifierItemService = modifierItemService;
            _logger = logger;
        }

        [HttpPut("publish-all")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> Publish( [FromQuery] string businessName)
        {

            var response = new BaseResponse { Status = true, Message = "All items are published." };

            var menuGroupResponse = await _menuGroupService.PublishMenuGroup(businessName);

            if(!menuGroupResponse.Status)
            {
                response.Status = menuGroupResponse.Status;
                response.Message = menuGroupResponse.Message;
            }

            var menuItemResponse = await _menuItemService.PublishMenuItem(businessName);
            if (!menuItemResponse.Status)
            {
                response.Status = menuItemResponse.Status;
                response.Message = menuItemResponse.Message;
            }

            var menu = await _menuService.PublishMenu(businessName);
            if (!menu.Status)
            {
                response.Status = menu.Status;
                response.Message = menu.Message;
            }

            var modifierGroupResponse = await _modifierGroupService.PublishModifierGroup(businessName);
            if (!modifierGroupResponse.Status)
            {
                response.Status = modifierGroupResponse.Status;
                response.Message = modifierGroupResponse.Message;
            }

            var modifierItemResponse = await _modifierItemService.PublishModifierItem(businessName);
            if (!modifierItemResponse.Status)
            {
                response.Status = modifierItemResponse.Status;
                response.Message = modifierItemResponse.Message;
            }

            return response.Status ? Ok(response) : BadRequest(response);

            
        }
    }
}
