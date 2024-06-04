using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;


namespace SUDLife_GetApplicationNumber.Models.Request
{
    public class ClsGetAppNumberPlainRequest
    {
        [Required]
        public string Source { get; set; }
        public AppNoReq AppNoReq { get; set; }
    }
    public class AppNoReq
    {
        [Required]
        public string CustomerID { get; set; }
        public string ApplicationNo { get; set; }
        public string ProductName { get; set; }

        /// <summary>
        /// Mapping Values
        /// 1.  "pos",
        /// 2.  "individual",
        /// 3.  "credit life"
        /// </summary>
        [Required]
        public string ProductType { get; set; }
        [DataType(DataType.Date)]
        [Required]
        [RegularExpression("dd-MM-yyyy")]
        public string DOB { get; set; }
        [Required]
        public string Source { get; set; }
        [Required]
        public string TransactionId { get; set; }
        public ChannelDetails ChannelDetails { get; set; }
        public BankAcctDetails BankAcctDetails { get; set; }
        public CustomerDetails CustomerDetails { get; set; }
    }
    public class ChannelDetails
    {
        [Required]
        public string ChannelType { get; set; }
        [Required]
        public string ChannelCode { get; set; }
    }

    public class BankAcctDetails
    {
        [Required]
        public string AccountNumber { get; set; }

        [Required]
        public string BranchCode { get; set; }

        [Required]
        public string IFSCCode { get; set; }

        [Required]
        public string AccountType { get; set; }

        [Required]
        [JsonProperty("Contact number")]
        public string ContactNumber { get; set; }
    }

    public class CustomerDetails
    {
        [Required]
        public string CustName { get; set; }
        [Required]
        public string CustAddress1 { get; set; }
        public string CustAddress2 { get; set; }
        public string CustAddress3 { get; set; }
        public string CustCity { get; set; }
        public string CustState { get; set; }
        public string Pincode { get; set; }
        [Required]
        public string CustMobile { get; set; }
        public string CustLandline { get; set; }
        [Required]
        public string CustEmail { get; set; }
    }

}
