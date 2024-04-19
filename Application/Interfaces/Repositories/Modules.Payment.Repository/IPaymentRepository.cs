using Domain.Domain.Modules.MenuSettings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Repositories.Modules.Payment.Repository
{
    public interface IPaymentRepository
    {
        Task<Domain.Domain.Modules.PaymentGateway.Payment> CreatePayment(Domain.Domain.Modules.PaymentGateway.Payment payment);
        //Task<Domain.Domain.Modules.PaymentGateway.Payment> UpdatePaymentAsync(Domain.Domain.Modules.PaymentGateway.Payment payment);
        Task<Domain.Domain.Modules.PaymentGateway.Payment> GetPaymentByBusinessNameAsync(string paymentReferenceNumber, string businessName);
        Task<IEnumerable<Domain.Domain.Modules.PaymentGateway.Payment>> GetAllPaymentsByBusinessNameAsync(string businessName);
    }
}
