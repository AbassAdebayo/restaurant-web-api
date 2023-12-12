using Application.DTOs;
using Application.DTOs.MenuSettingDtos;
using Application.DTOs.TableManagementDtos;
using Application.Interfaces.Services.Modules.TableManagement.Services;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Filters;

namespace WebApi.Controllers.TableManagementModule
{
    [Route("api/table")]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpPost("add-tables-for-preview/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> AddTables([FromRoute] string token, [FromQuery] string branchName, [FromBody] CreateTablesRequestModel model)
        {
            var response = await _tableService.AddTablesPreviewAsync(token, branchName, model);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPost("save-table-as/{token}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> SaveTableAs([FromRoute] string token, [FromQuery] string branchName)
        {
            var response = await _tableService.SaveTablesAsAsync(token, branchName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        
        //[HttpGet("get-all-tables-by-company-name")]
        //[ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        //public async Task<IActionResult> GetAllTables([FromQuery] string businessName)
        //{
        //    var response = await _tableService.GetAllTablesByCompanyNameAsync(businessName);
        //    return response.Status ? Ok(response) : BadRequest(response);
        //}

        [HttpGet("get-all-tables")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetAllTables([FromQuery] string businessName)
        {
            var response = await _tableService.GetTablesDataByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-grouped-tables-for-branchname")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetGroupedTablesForBranchName([FromQuery] string businessName)
        {
            var response = await _tableService.GetGroupedTablesFrorBranchByCompanyNameAsync(businessName);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-table-by-id/{Id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetTableById([FromRoute] Guid Id)
        {
            var response = await _tableService.GetTableByIdAsync(Id);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpPut("enable-or-disable-table/{Id:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> EnableOrDisableTable([FromRoute] Guid Id, [FromBody] bool isActive)
        {
            var response = await _tableService.EnableOrDisableTable(Id, isActive);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpGet("get-table-by-tableId/{tableId}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> GetTableByTableId([FromRoute] string tableId)
        {
            var response = await _tableService.GetTableByTableIdAsync(tableId);
            return response.Status ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("delete-menu/{tableId:guid}")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BaseResponse))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ValidationResultModel))]
        public async Task<IActionResult> DeleteTable([FromRoute] Guid tableId)
        {
            var response = await _tableService.DeleteTableAsync(tableId);
            return response.Status ? Ok(response) : BadRequest(response);
        }
    }
}
