using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpPost("add-menu/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddMenu([FromRoute] string token, [FromBody] CreateMenuRequestModel model)
        {
            var response = await _menuService.AddMenuAsync(token, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-menus-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllMenus([FromQuery] string businessName)
        {
            var response = await _menuService.GetAllMenusByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpGet("get-menu-by-id/{menuId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetMenuById([FromRoute] Guid menuId)
        //{
        //    var response = await _menuService.GetMenuByIdAsync(menuId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpDelete("delete-menu/{menuId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> DeleteMenu([FromRoute] Guid menuId)
        //{
        //    var response = await _menuService.DeleteMenuAsync(menuId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpPut("edit-menu/{menuId:guid}/{token}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> EditMenu([FromRoute] Guid menuId, [FromRoute] string token, [FromBody] EditMenuRequestModel model)
        //{
        //    var response = await _menuService.EditMenuAsync(token, menuId, model);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        
    }
}
