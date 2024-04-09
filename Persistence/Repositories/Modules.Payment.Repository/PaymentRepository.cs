using Application.Interfaces.Repositories.Modules.Payment.Repository;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.Modules.Payment.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationContext _context;

        public PaymentRepository(ApplicationContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }


        public async Task<IEnumerable<Domain.Domain.Modules.PaymentGateway.Payment>> GetAllPaymentsByBusinessNameAsync(string businessName)
        {
            return await _context.Payments
            .Include(o => o.Order)
            .Where(b => b.BusinessName == businessName)
             .AsNoTracking()
            .ToListAsync();
        }

        public async Task<Domain.Domain.Modules.PaymentGateway.Payment> 
            GetPaymentByBusinessNameAsync(string paymentReferenceNumber, string businessName)
        {
            return await _context.Payments
            .Include(o => o.Order)
            .Where(p => p.PaymentReferenceNumber == paymentReferenceNumber && p.BusinessName == businessName)
            .SingleOrDefaultAsync();

        }


    }
}
