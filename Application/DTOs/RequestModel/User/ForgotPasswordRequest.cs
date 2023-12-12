using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestModel.User
{
    public class ForgotPasswordRequest
    {
        public string Email { get; set; }
    }

    public class ChangePincode
    {
        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string ConfirmPincode { get; set; }


    }


    public class ResetPasswordAndPincodeRequest
    { 
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string Pincode { get; set; }

        [Required(ErrorMessage = "Pincode is required.")]
        [StringLength(4, MinimumLength = 4, ErrorMessage = "Pincode must be 4 digits.")]
        public string ConfirmPincode { get; set; }

    }

   


}
