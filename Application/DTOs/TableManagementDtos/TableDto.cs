using Application.DTOs.TicketDtos;
using Domain.Domain.Modules.MenuSettings;
using Domain.Domain.Modules.Order;
using Domain.Domain.Modules.Tables;
using Domain.Domain.Modules.Users.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TableManagementDtos
{
    public class TableDto
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; }
        public string TableId { get; set; }
        public string QrCode { get; set; }
        public TableStatus? Status { get; set; }   
        public string CompanyName { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public bool? IsActive { get; set; }
        public string? BranchName { get; set; }
        public Guid? MenuId { get; set; }
        public Menu? Menu { get; set; }
        public ICollection<GuestDto> Guests { get; set; } = new HashSet<GuestDto>();
        public ICollection<TabDto> Tabs { get; set; } = new HashSet<TabDto>();
    }

    public class TableDataResponseDto
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; }
        public string TableId { get; set; }
        public string QrCode { get; set; }
        public bool? IsActive { get; set; }
        public string? BranchName { get; set; }
    }

    public class GroupedTablesResponseDto
    {
        public string? BranchName { get; set; }
        public List<TableInfo> TableNumbers { get; set; }
    }

    public class TableInfo
    {
        public Guid Id { get; set; }
        public string TableNumber { get; set; }
    }

    public class CreateTablesRequestModel
    {
        public int TableNumber { get; set; }
    }

   
    public class SaveAsRequestModel
    {
        public string BranchName { get; set; }
    }
    
    public class EditTableRequestModel
    {
        public int TableNumber { get; set; }
    }
}
