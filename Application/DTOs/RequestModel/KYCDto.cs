using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.RequestModel
{
    public class KYCDto
    {
        public string CompanyName { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string Category { get; set; }
        public string VatNumber { get; set; }
        public string WebPage { get; set; }
        public string BusinessAddressLine1 { get; set; }
        public string BusinessAddressLine2 { get; set; }
        public string City { get; set; }
        public DateTime BusinessFiscalYearFrom { get; set; }
        public DateTime BusinessFiscalYearTo { get; set; }

        //Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegisteredHomeAddress { get; set; }
        public string RegisteredCity { get; set; }
        public string Country { get; set; }

        //Payout & Bank Details
        public string AccountHolderOrBusinessName { get; set; }
        public string BVN { get; set; }
        public string BankCountry { get; set; }

    }

    public class KYCRequestModel
    {
        public Guid SuperAdminId { get; set; }
        public string CompanyName { get; set; }
        public string BusinessPhoneNumber { get; set; }
        public string Category { get; set; }
        public string VatNumber { get; set; }
        public string WebPage { get; set; }
        public string BusinessAddressLine1 { get; set; }
        public string BusinessAddressLine2 { get; set; }
        public string City { get; set; }
        public DateTime BusinessFiscalYearFrom { get; set; }
        public DateTime BusinessFiscalYearTo { get; set; }

        //Personal Information
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string RegisteredHomeAddress { get; set; }
        public string RegisteredCity { get; set; }
        public string Country { get; set; }

        //Payout & Bank Details
        public string AccountHolderOrBusinessName { get; set; }
        public string BVN { get; set; }
        public string BankCountry { get; set; }

    }
}
