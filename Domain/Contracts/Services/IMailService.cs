public interface IMailService
{
    public Task<bool> SendForgotPasswordMail(string email, string name, string passwordResetLink);

    public Task<bool> SendChangePasswordAndPincodeMail(string email, string employeeUserPassword, string employeeUserPincode);
    
    public Task<bool> SendChangePasswordMail(string email, string name, string userPassword);

    public Task<bool> SendVerificationMail(string email, string name, string token);

    public Task<bool> SendInvitationMail(string email, string name, string token, IList<string> Roles);
    
    public Task<bool> SendUserUpdateMail(string email, string oldName, string newName);
}