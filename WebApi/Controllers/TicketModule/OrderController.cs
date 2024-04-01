using Application.DTOs;
using Application.DTOs.TicketDtos;
using Application.Interfaces.Services.Modules.Ticket.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.TicketModule
{
    [Route("api/order")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpPost("make-order/{tabId:guid}/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> MakeOrder([FromRoute] string token, [FromRoute] Guid tabId, [FromBody] CreateOrderRequestModel model)
        {
            var response = await _orderService.AddOrderAsync(model, tabId, token);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
