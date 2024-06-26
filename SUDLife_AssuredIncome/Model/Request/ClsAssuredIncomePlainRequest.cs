
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
namespace SUDLife_AssuredIncome.Model.Request
{
    public class ClsAssuredIncomePlainRequest
    {
            [DataType(DataType.Date)]
            [Required]
            [RegularExpression("yyyy-MM-dd")]
            public string? InwardDate { get; set; }

            [Required]
            public string? ApplicationNo { get; set; }
            ///<example>BOI</example>
            [Required]

            public string? Source { get; set; }

            [Required]
            public string CustomerID { get; set; }
            public AddField AddField { get; set; }
            public ApplicantDetails ApplicantDetails { get; set; }
            public ProposerDetails ProposerDetails { get; set; }
            public BankDetailsAssuredIncome BankDetails { get; set; }
            [Required]
            public int PolicyTerm { get; set; }
            [Required]
            public int PremiumPaymentTerm { get; set; }
            [Required]
            public string AnnualPayout { get; set; }


            /// <summary> 
            ///  Mapping Values for PremiumPaymentModes:
            /// 1.  "Annual"
            /// 2.  "Semi-Annual"
            /// 3.  "Quarterly (ecs/ si)"
            /// 4.  "Monthly (ecs/ si)"
            /// 5.  "Single"
            /// </summary>
            [Required]
            public string PremiumPaymentModes { get; set; }
            /// <summary>
            /// Mapping Values for DefermentPeriod:
            ///1.	“0”
            ///2.	“5” 
            /// </summary>
            [Required]
            public string DefermentPeriod { get; set; }
            /// <summary> 
            ///  Mapping Values for Payoutperiod:
            /// 1.  "10"
            /// 2.  "15"
            /// 3.  "20"
            /// 4.  "25"
            /// </summary>
            [Required]
            public string Payoutperiod { get; set; }
            [Required]
            public string ADTPDRiderOpted { get; set; }

            public int ADTPDRiderSA { get; set; }
            [Required]
            public string COVIDRiderOpted { get; set; }

            public int COVIDRiderSA { get; set; }
            /// <summary>
            /// Mapping Values for StandardAgeProof:
            ///1.	“Yes”
            ///2.	“No” 
            /// </summary>
            [Required]
            public string StandardAgeProof { get; set; }
            [Required]
            public string KerelaFloodsCESSApplicable { get; set; }
            /// <summary>
            ///  Mapping Values for DistributionChannel:
            ///1.	“Corporate Agency”
            ///2.	“Brokers”
            ///3.	“Agency”
            ///4.	“Direct Channel”
            ///5.	“Insurance Marketing Firm”
            /// </summary>
            [Required]
            public string DistributionChannel { get; set; }

            /// <summary>
            ///  Mapping Values for StaffDiscount:
            /// 1.  "Yes"
            /// 2.  "No"
            /// </summary>
            [Required]
            public string StaffDiscount { get; set; }

        }


        public class BankDetailsAssuredIncome
        {
            public string IFSCCode { get; set; }
            public string MICRCode { get; set; }
            public string BankName { get; set; }
            public string BankBranchName { get; set; }
            public string AccountHolderName { get; set; }

            [JsonProperty("BankA/CNo.")]
            public string BankACNo { get; set; }
            public string TypeofAccount { get; set; }
        }
    public class Dummy
    {
        public string DummyName { get; set; } = string.Empty;
        public string DummyValue { get; set; } = string.Empty;
    }

    public class AddField
    {
        public List<Dummy> Dummy { get; set; } = new List<Dummy>();
    }

    public class ApplicantDetails
    {
        [Required]
        public string ApplicantFName { get; set; } = string.Empty;
        [Required]
        public string ApplicantLName { get; set; } = string.Empty;
        [Required]
        public string ApplicantDateOfBirth { get; set; } = string.Empty;
        [Required]
        public string ApplicantGender { get; set; } = string.Empty;
        [Required]
        public string ApplicantEmail { get; set; } = string.Empty;
        [Required]
        public string ApplicantContactNumber { get; set; } = string.Empty;
        public string ApplicantCity { get; set; } = string.Empty;
        public string ApplicantState { get; set; } = string.Empty;
    }

    public class ProposerDetails
    {
        [Required]
        public string IsProposersameasApplicant { get; set; } = string.Empty;
        [Required]
        public string ProposerFName { get; set; } = string.Empty;
        public string ProposerLName { get; set; } = string.Empty;
        [Required]
        public string ProposerDateOfBirth { get; set; } = string.Empty;
        [Required]
        public string ProposerGender { get; set; } = string.Empty;
        [Required]
        public string ProposerEmail { get; set; } = string.Empty;
        [Required]
        public string ProposerContactNumber { get; set; } = string.Empty;
        public string ProposerCity { get; set; } = string.Empty;
        public string ProposerState { get; set; } = string.Empty;
    }
}
