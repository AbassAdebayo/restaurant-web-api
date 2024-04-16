using Application.DTOs;
using Application.DTOs.PaymentDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.Modules.Payment.Service
{
    public interface IPaymentService
    {
        Task<BaseResponse<PaymentResponse>> InitiatePayment(CreatePaymentRequestModel model, Guid orderId);
        Task<BaseResponse<PaymentResponse>> GetTransactionRecieptAsync(string transactionReference);
        Task<string> VerifyPayment(UpdatePaymentRequestModel model, Guid orderId);

    }
}
