using Application.DTOs;
using Application.DTOs.PaymentDtos;
using Application.Implementations.Modules.Ticket.Services;
using Application.Interfaces.Modules.RolePermission.Repository;
using Application.Interfaces.Repositories.Modules.Users.IRepository;
using Application.Interfaces;
using Application.Interfaces.Services.Modules.Payment.Service;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Interfaces.Repositories.Modules.Payment.Repository;
using Application.Interfaces.Repositories.Modules.Ticket.Repositories;

namespace Persistence.PaymentGateway
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IIdentityService _identityService;
        private readonly IUserRepository _userRepository;
        private readonly IRolePermissionsRepository _rolePermissionsRepository;
        private readonly ILogger<PaymentService> _logger;

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IIdentityService identityService, IUserRepository userRepository, 
            IRolePermissionsRepository rolePermissionsRepository, ILogger<PaymentService> logger)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _rolePermissionsRepository = rolePermissionsRepository ?? throw new ArgumentNullException(nameof(rolePermissionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<BaseResponse<PaymentResponse>> GetTransactionRecieptAsync(string transactionReference)
        {
            throw new NotImplementedException();
        }

        public Task<BaseResponse<PaymentResponse>> InitiatePayment(CreatePaymentRequestModel model, Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<string> VerifyPayment(UpdatePaymentRequestModel model, Guid orderId)
        {
            throw new NotImplementedException();
        }
    }
}
