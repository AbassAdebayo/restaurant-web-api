using Domain.Domain.Modules.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.TableManagementDtos
{
    public class GuestDto
    {
        public Guid Id { get; set; }
        public int NumberOfGuest { get; set; }
        public string CompanyName { get; set; }
        public Guid TableId { get; set; }
        public Table? Table { get; set; }
        
    }

    public class AddNumberOfGuestRequestModel
    {
        public int NumberOfGuest { get; set; }
    }

    public class EditNumberOfGuestRequestModel
    {
        public int NumberOfGuest { get; set; }
    }
}
