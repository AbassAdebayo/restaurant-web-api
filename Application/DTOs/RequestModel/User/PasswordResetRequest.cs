using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestModel.User
{
    public class PasswordResetRequest
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
    }

    public class PasswordResetRequestModel
    {
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

    }
}
