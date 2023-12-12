using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.Implementations.Modules.MenuSettings.Services;
using Application.Interfaces.Modules.MenuSettings.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.MenuSettingsModule
{
    [Route("api/menu")]
    public class PriceListController : ControllerBase
    {
        private readonly IPriceListService _priceListService;

        public PriceListController(IPriceListService priceListService)
        {
            _priceListService = priceListService;
        }

        [HttpGet("get-pricelist-by-company-name")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetPriceList([FromQuery] string businessName)
        {
            var response = await _priceListService.GetPriceListAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("edit-price-list/{menuItemId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EditPriceListItem([FromRoute] Guid menuItemId, [FromRoute] string token, [FromBody] EditPriceListItemRequestModel model)
        {
            var response = await _priceListService.EditPriceListItemAsync(token, menuItemId, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
