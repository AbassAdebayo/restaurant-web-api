using Application.DTOs;
using Application.DTOs.TableManagementDtos;
using Application.Implementations.Modules.TableManagement.Services;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.TableManagementModule
{
    [Route("api/tab")]
    public class TabController : ControllerBase
    {
        private readonly ITabService _tabService;

        public TabController(ITabService tabService)
        {
            _tabService = tabService;
        }

        [HttpPost("add-tabname/{token}/{tableId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddTab([FromRoute] string token, [FromRoute] Guid tableId, [FromBody] AddTabRequestModel model)
        {
            var response = await _tabService.AddTabAsync(token, tableId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-tabs")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllTabs([FromQuery] string businessName)
        {
            var response = await _tabService.GetAllTabsByCompanyAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-all-tabs-for-table")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllTabsForTable([FromQuery] Guid tableId)
        {
            var response = await _tabService.GetTabsForTableAsync(tableId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-tab-by-Id/{tabId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetTabById([FromRoute] Guid tabId)
        {
            var response = await _tabService.GetTabByIdAsync(tabId);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
