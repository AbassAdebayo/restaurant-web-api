using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PaymentDtos
{
    public class PaymentDto
    {
        public Guid OrderId { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ResponseContent { get; set; }
    }

    public class CreatePaymentRequestModel
    {
        public decimal Amount { get; set;}
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }

    public class UpdatePaymentRequestModel
    {
        public bool Successful { get; set; }
    }
}
