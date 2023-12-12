using Domain.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Exceptions.Messaging;
using Persistence.Exceptions.TemplateEngine;
using Persistence.Messaging.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.MailService
{
    public class MailingService : IMailService
    {
        private readonly IMailSender _mailSender;
        private readonly IRazorEngine _razorEngibe;
        private readonly EmailConfiguration _emailConfiguration;
        private readonly ILogger<IMailSender> _logger;

        public MailingService(IMailSender mailSender, IRazorEngine razorEngibe, IOptions<EmailConfiguration> options, ILogger<IMailSender> logger)
        {
            _mailSender = mailSender;
            _razorEngibe = razorEngibe;
            _emailConfiguration = options.Value;
            _logger = logger;
        }

        public Task<bool> SendChangePasswordAndPincodeMail(string email, string employeeUserPassword, string employeeUserPincode)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendChangePasswordMail(string email, string name, string userPassword)
        {
            try
            {
                var model = new ChangePassword()
                {
                    Name = name,
                    Email = email,
                };
                var mailBody = await _razorEngibe.ParseAsync("ChangePasswordMail", model);
                return await _mailSender.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.ChangePasswordSubject, mailBody);
            }
            catch (RazorEngineExecption ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendForgotPasswordMail(string email, string name, string passwordResetLink)
        {
            try
            {
                var model = new ForgotPassword()
                {
                    Name = name,
                    Email = email,
                    PasswordResetLink = passwordResetLink
                };
                var mailBody = await _razorEngibe.ParseAsync("ForgotPasswordMail", model);
                return await _mailSender.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.ForgotPasswordSubject, mailBody);
            }
            catch (RazorEngineExecption ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public async Task<bool> SendInvitationMail(string email, string name, string token, IList<string> Roles)
        {
            try
            {
                var model = new SendInvitation()
                {
                    Name = name,
                    Email = email,
                    Token = token,
                    Roles = Roles
                };
                var mailBody = await _razorEngibe.ParseAsync("SendInvitationMail", model);
                return await _mailSender.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.InvitationSubject, mailBody);
            }
            catch (RazorEngineExecption ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        public Task<bool> SendUserUpdateMail(string email, string oldName, string newName)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> SendVerificationMail(string email, string name, string token)
        {
            try
            {
                var model = new SendVerification()
                {
                    Name = name,
                    Email = email,
                    Token = token
                };
                var mailBody = await _razorEngibe.ParseAsync("SendVerificationMail", model);
                return await _mailSender.Send(_emailConfiguration.FromEmail, _emailConfiguration.FromName, email, name, _emailConfiguration.VerificationSubject, mailBody);
            }
            catch (RazorEngineExecption ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            catch (MailSenderException ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }
    }
}
