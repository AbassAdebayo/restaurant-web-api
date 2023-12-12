using Application.DTOs;
using Domain.Domain.Modules.Users.Entities.Enums;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;
using System.Reflection;
using WebApi.Filters;

namespace WebApi.Controllers
{
    [Route("api/enum")]
    [ApiController]
    public class EnumController : ControllerBase
    {
        [HttpGet("get-employee-type")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetEmployeeType()
        {
            var employeeTypes = Enum.GetValues(typeof(EmployeeType)).Cast<int>().ToList();

            List<string> employee = new List<string>();
            foreach (var item in employeeTypes)
            {
                employee.Add(Enum.GetName(typeof(EmployeeType), item));
            }
            return Ok(employee);
        }

        [HttpGet("get-menu-item-pricing-option")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetMenuItemPricingOption()
        {
            var menuItemPricingOptions = Enum.GetValues(typeof(MenuItemPricingOption)).Cast<int>().ToList();

            List<string> menuItemPricing = new List<string>();
            foreach (var item in menuItemPricingOptions)
            {
                menuItemPricing.Add(Enum.GetName(typeof(MenuItemPricingOption), item));
            }
            return Ok(menuItemPricing);
        }

        [HttpGet("get-modifier-rules")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetModifierRules()
        {
            var enumValuesWithCustomChannels = Enum.GetValues(typeof(ModifierRules))
             .Cast<ModifierRules>()
             .Select(rule => new
             {
                 EnumValue = rule,
                 CustomFormat = rule.ToCustomFormat()
             });

            return Ok(enumValuesWithCustomChannels);
        }

        [HttpGet("get-order-channels")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetOrderChannels()
        {
            var enumValuesWithCustomChannels = Enum.GetValues(typeof(OrderChannel))
             .Cast<OrderChannel>()
             .Select(order => new
             {
                 EnumValue = order,
                 CustomFormat = order.ToCustomFormat()
             });

            return Ok(enumValuesWithCustomChannels);
        }

        [HttpGet("get-channels")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized, Type = typeof(BaseResponse))]
        public async Task<IActionResult> GetChannels()
        {
            var enumValuesWithCustomChannels = Enum.GetValues(typeof(Channels))
              .Cast<Channels>()
              .Select(posChannel => new
              {
                  EnumValue = posChannel,
                  CustomFormat = posChannel.ToCustomFormat()
              });

            return Ok(enumValuesWithCustomChannels);
        }
    }


    
}
