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
using Microsoft.Extensions.Configuration;
using Domain.Domain.Modules.Order;
using MySqlX.XDevAPI;
using System.Text.Json;
using Domain.Domain.Modules.PaymentGateway;

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
        private readonly IConfiguration _configuration;

        private static readonly HttpClient _client = new HttpClient();

        public PaymentService(IPaymentRepository paymentRepository, IOrderRepository orderRepository, IIdentityService identityService, IUserRepository userRepository, 
            IRolePermissionsRepository rolePermissionsRepository, ILogger<PaymentService> logger, IConfiguration configuration)
        {
            _paymentRepository = paymentRepository ?? throw new ArgumentNullException(nameof(paymentRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _rolePermissionsRepository = rolePermissionsRepository ?? throw new ArgumentNullException(nameof(rolePermissionsRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public Task<BaseResponse<PaymentResponse>> GetTransactionRecieptAsync(string transactionReference)
        {
            throw new NotImplementedException();
        }

        public async Task<BaseResponse<PaymentResponse>> InitiatePayment(CreatePaymentRequestModel model, Guid orderId)
        {
            var mySecretKey = _configuration["MySecretKey-Paystack"];

            var order = await _orderRepository.GetOrderByIdAsync(orderId);
            if (order == null)
            {
                _logger.LogError("The Order accessed couldn't be fetched!");
                return new BaseResponse<PaymentResponse>
                {
                    Message = "The Order accessed couldn't be fetched!",
                    Status = false
                };
            }
            var customerName = order.CustomerName;
            var customerReferenceNumber = order.CustomerReferenceNumber;

            var url = "https://api.paystack.co/transaction/initialize";

            // Set reciever account details
            var recipients = new
            {
                account_number = "0234032001",
                bank_code = "737",  // Bank code for the receiver's bank (e.g., GTBank)
                                    // Add any other recipient details as required
            };
            var paymentReferenceNumber = GeneratePaymentReference();
            var payload = new
            {
                amount = model.Amount * 100,  // Set the amount in kobo (e.g., 5000 = ₦5000)
                email = model.Email,
                phone = model.PhoneNumber,
                reference = paymentReferenceNumber,
                callback_url = $"http://127.0.0.1:5500/dashboard/receipt.html?id={orderId}",
                customer_name = customerName
                
                // recipient = recipients,
                // card = new
                // {
                //     number = "4084084084084081",  // Card number
                //     cvv = "123",                  // CVV
                //     expiry_month = "01",          // Expiry month
                //     expiry_year = "24"            // Expiry year
                // }
            };
            var jsonPayload = Newtonsoft.Json.JsonConvert.SerializeObject(payload);

            // Create the HTTP request
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            request.Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
            request.Headers.Add("Authorization", $"Bearer {mySecretKey}");

            // Send the request and retrieve the response
            var response = await _client.SendAsync(request);
            var responseContent = await response.Content.ReadAsStringAsync();
            var responses = JsonSerializer.Deserialize<BaseResponse<PaymentResponse>>(responseContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Process the response
            if (response.IsSuccessStatusCode)
            {
                // Payment initiation successful
                var payment = new Payment
                {
                    OrderId = orderId,
                    CustomerName = customerName,
                    PaymentReferenceNumber = responseContent.Split("\"reference\":")[1].Split("\"")[1],
                };
                await _paymentRepository.CreatePayment(payment);
                return responses;
            }
            else
            {
                // Payment initiation failed
                throw new Exception($"Payment initiation failed. Response: {responseContent}");
            }
        }

        public Task<string> VerifyPayment(UpdatePaymentRequestModel model, Guid orderId)
        {
            throw new NotImplementedException();
        }

        private string GeneratePaymentReference()
        {
            return $"PAY-{Guid.NewGuid().ToString().Substring(0, 6)}/{DateTime.UtcNow}";
        }
    }

    
}
