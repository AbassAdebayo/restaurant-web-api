using MassTransit;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.MenuSettingDtos
{
    public class TimeSpecificPriceOptionDto
    {
        public Guid Id { get; set; }
        public Guid MenuItemId { get; set; }
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }

    public class TimeSpecificPriceOptionCreateRequestModel
    {
        public decimal BasePrice { get; set; }
        public decimal DiscountedPrice { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
    }
}
