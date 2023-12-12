public interface IMailSender
{
    Task<bool> Send(string from, string fromName, string to, string toName, string subject, string message, IDictionary<string, Stream> attachments = null);

    Task<bool> SendBulk(string from, string fromName, IDictionary<string, string> tos, string subject, string message, IDictionary<string, Stream> attachments = null);
}