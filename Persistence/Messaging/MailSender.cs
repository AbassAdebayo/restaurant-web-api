using Domain.Configurations;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Persistence.Exceptions.Messaging;
using sib_api_v3_sdk.Api;
using sib_api_v3_sdk.Client;
using sib_api_v3_sdk.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.IdentityModel.Protocols;
using System.Configuration;
using Microsoft.Extensions.Configuration;

namespace Persistence.Messaging
{
    public class MailSender : IMailSender
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<IMailSender> _logger;

        public MailSender(IConfiguration configuration, ILogger<IMailSender> logger)
        {
            _configuration = configuration;
            _logger = logger;
            
            
        }

        public async Task<bool> Send(string from, string fromName, string to, string toName, string subject, string message, IDictionary<string, Stream> attachments = null)
        {
            
            var smtpApiKey = _configuration["SmtpApiKey"];

            var apiInstance = new TransactionalEmailsApi();
            var sendSmtpEmail = new SendSmtpEmail
            {
                HtmlContent = message,
                Subject = subject,
                Sender = new SendSmtpEmailSender(fromName, from),
                To = new List<SendSmtpEmailTo>() { new SendSmtpEmailTo(to, toName) }
            };

            if (attachments != null)
            {
                foreach (var attachment in attachments)
                {
                    sendSmtpEmail.Attachment.Add(new SendSmtpEmailAttachment(content: ReadFully(attachment.Value), name: attachment.Key));
                }
            }

            if (!string.IsNullOrEmpty(smtpApiKey))
            {
                sib_api_v3_sdk.Client.Configuration.Default.AddApiKey("api-key", smtpApiKey);
                try
                {
                    await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError("Exception when calling TransactionalEmailsApi.SendTransacEmail: " + e.Message);
                    throw new MailSenderException(e.Message, e);
                }
            }

            _logger.LogError("SMTP API Key is not configured.");
            throw new MailSenderException("SMTP API Key is not configured.");


        }

        public async Task<bool> SendBulk(string from, string fromName, IDictionary<string, string> tos, string subject, string message, IDictionary<string, Stream> attachments = null)
        {
            var smtpApiKey = _configuration["SmtpApiKey"];

            var apiInstance = new TransactionalEmailsApi();
                var sendSmtpEmail = new SendSmtpEmail
                {
                    Sender = new SendSmtpEmailSender(fromName, from),
                    To = tos.Select(a => new SendSmtpEmailTo(a.Key, a.Value)).ToList(),
                };


                if (attachments != null)
                {
                    foreach (var attachment in attachments)
                    {
                        sendSmtpEmail.Attachment.Add(new SendSmtpEmailAttachment(content: ReadFully(attachment.Value), name: attachment.Key));
                    }
                }

            if (!string.IsNullOrEmpty(smtpApiKey))
            {
                sib_api_v3_sdk.Client.Configuration.Default.AddApiKey("api-key", smtpApiKey);

                try
                {
                    await apiInstance.SendTransacEmailAsync(sendSmtpEmail);
                    return true;
                }
                catch (Exception e)
                {
                    _logger.LogError("Exception when calling TransactionalEmailsApi.SendTransacEmail: " + e.Message);
                    throw new MailSenderException(e.Message, e);
                }
            }
            _logger.LogError("SMTP API Key is not configured.");
            throw new MailSenderException("SMTP API Key is not configured.");
            


        } 


        private static byte[] ReadFully(Stream input)
        {
            using MemoryStream ms = new();
            input.CopyTo(ms);
            return ms.ToArray();
        }
    }
}
