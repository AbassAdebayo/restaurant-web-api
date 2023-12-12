using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Configurations
{
    public class EmailConfiguration
    {

        public string FromEmail { get; set; } 
        public string FromName { get; set; } = "troo-x";
        public string ForgotPasswordSubject { get; set; } = "ForgotPassword";
        public string ChangePasswordSubject { get; set; }
        public string ChangePincodeSubject { get; set; }
        public string ResetPasswordSubject { get; set; }
        public string VerificationSubject { get; set; } = "Verification";
        public string InvitationSubject { get; set; } = "Invitation";

    }
}
