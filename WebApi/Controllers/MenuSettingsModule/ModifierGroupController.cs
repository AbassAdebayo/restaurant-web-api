using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/modifiergroup")]
    public class ModifierGroupController : ControllerBase
    {
        private readonly IModifierGroupService _modifierGroupService;

        public ModifierGroupController(IModifierGroupService modifierGroupService)
        {
            _modifierGroupService = modifierGroupService;
        }

        [HttpPost("add-menugroup/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddModifierGroup([FromRoute] string token, [FromBody] CreateModifierGroupRequestModel model)
        {
            var response = await _modifierGroupService.AddModifierGroupAsync(token, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-modifiergroups-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllModifierGroups([FromQuery] string businessName)
        {
            var response = await _modifierGroupService.GetAllModifierGroupsByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        //[HttpGet("get-modifiergroup-by-id/{modifiergroupId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetModifierGroupById([FromRoute] Guid modifiergroupId)
        //{
        //    var response = await _modifierGroupService.GetModifierGroupByIdAsync(modifiergroupId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        //[HttpDelete("delete-modifiergroup/{modifiergroupId:guid}")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> DeleteModifierGroup([FromRoute] Guid modifiergroupId)
        //{
        //    var response = await _modifierGroupService.DeleteModifierGroupAsync(modifiergroupId);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        [HttpPut("edit-modifiergroup/{modifiergroupId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EditModifierGroup([FromRoute] Guid modifiergroupId, [FromRoute] string token, [FromBody] EditModifierGroupRequestModel model)
        {
            var response = await _modifierGroupService.EditModifierGroupAsync(token, modifiergroupId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }


        [HttpPut("apply-modifier-rules-to-modifiergroup/{modifiergroupId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> ApplyModifierRulesToModifierGroup([FromRoute] Guid modifiergroupId, [FromBody] ApplyModifierRulesToModifierGroupRequestModel model)
        {
            var response = await _modifierGroupService.ApplyModifierRulesToModifierGroupAsync(modifiergroupId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
