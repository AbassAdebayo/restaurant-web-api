using Application.DTOs.TicketDtos;
using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TableManagementDtos
{
    public class TabDto
    {
        public Guid TabId { get; set; }
        public string TabName { get; set; }
        public string CompanyName { get; set; }
        public Guid TableId { get; set; }
        public Guest? Table { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime? ModifiedOn { get; set; }
        public string CreatedBy { get; set; }
        public ICollection<OrderDto> Orders { get; set; } = new HashSet<OrderDto>();
    }

    public class AddTabRequestModel
    {
        public string TabName { get; set; }
    }
}
