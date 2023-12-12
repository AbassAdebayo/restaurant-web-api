using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class RangePriceOptionDto
    {
        public Guid Id { get; set; } 
        public Guid MenuItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class RangePriceOptionCreateRequestModel
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
