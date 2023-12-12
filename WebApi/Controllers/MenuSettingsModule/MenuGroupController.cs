using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/menugroup")]
    public class MenuGroupController : ControllerBase
    {
        private readonly IMenuGroupService _menuGroupService;

        public MenuGroupController(IMenuGroupService menuGroupService)
        {
            _menuGroupService = menuGroupService;
        }


        [HttpPost("add-menugroup/{menuId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddMenuGroup([FromRoute] string token, [FromRoute] Guid menuId, [FromBody] CreateMenuGroupRequestModel model)
        {
            var response = await _menuGroupService.AddMenuGroupAsync(token, menuId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-menugroups-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllMenuGroups([FromQuery] string businessName)
        {
            var response = await _menuGroupService.GetAllMenuGroupsByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpGet("get-menugroup-by-id/{menuGroupId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetMenuGroupById([FromRoute] Guid menuGroupId)
        //{
        //    var response = await _menuGroupService.GetMenuGroupByIdAsync(menuGroupId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpDelete("delete-menugroup/{menuGroupId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> DeleteMenuGroup([FromRoute] Guid menuGroupId)
        //{
        //    var response = await _menuGroupService.DeleteMenuGroupAsync(menuGroupId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpPut("edit-menugroup/{menuGroupId:guid}/{token}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> EditMenuGroup([FromRoute] Guid menuGroupId, [FromRoute] string token, [FromBody] EditMenuGroupRequestModel model)
        //{
        //    var response = await _menuGroupService.EditMenuGroupAsync(token, menuGroupId, model);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}
    }
}
