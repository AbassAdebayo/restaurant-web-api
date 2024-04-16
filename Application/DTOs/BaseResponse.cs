using Application.DTOs.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class BaseResponse
    {
        public string Message { get; set; }

        public bool Status { get; set; }
    }

    public class BaseResponse<T>
    {
        public string Message { get; set; }
        public bool Status { get; set; }
        public T Data { get; set; }
        public List<T> DataList { get; set; }

    }

    public class PaymentResponse
    {
        public string authorization_url { get; set; }
        public string access_code { get; set; }
        public string reference { get; set; }
        public PaymentDto paymentDto { get; set; }
    }

    
}
