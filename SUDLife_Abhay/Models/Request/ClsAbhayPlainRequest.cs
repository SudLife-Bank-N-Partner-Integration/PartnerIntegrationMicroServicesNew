using System.ComponentModel.DataAnnotations;

namespace SUDLife_Abhay.Models.Request
{
    public class ClsAbhayPlainRequest
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
        public ApplicantDetailsAbhay ApplicantDetails { get; set; }
        public ProposerDetailsAbhay ProposerDetails { get; set; }
        public ChannelDetails ChannelDetails { get; set; }
        [Required]
        public string PolicyTerm { get; set; }
        [Required]
        public string PremiumPaymentTerm { get; set; }
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
        ///  Mapping Values for DistributionChannel:
        ///1.	“Corporate Agency”
        ///2.	“Agency”
        ///3.	“Broker”
        ///4.	“Direct Marketing”
        ///5.	“Insurance Marketing Firm”
        ///6.	“Online”
        /// </summary>
        [Required]
        public string DistributionChannel { get; set; }

        /// <summary>
        ///  Mapping Values for StaffPolicy:
        /// 1.  "Yes" 
        /// 2.  "No"
        /// 3.  "Sud Life Staff/Family"
        /// </summary>
        [Required]
        public string StaffPolicy { get; set; }
        [Required]
        public string SumAssured { get; set; }
        [Required]
        public string AATPDRiderOpted { get; set; }
        [Required]
        public string AATPDRiderSA { get; set; }
        [Required]
        public string KerelaFloodsCESSApplicable { get; set; }
        [Required]
        public string StandardAgeProof { get; set; }
        /// <summary>
        ///  Mapping Values for Smoker:
        /// 1.  "Non - Smoker" 
        /// 2.  "Smoker"
        /// </summary>
        [Required]
        public string? Smoker { get; set; }
        /// <summary>
        ///  Mapping Values for BenefitOption:
        /// 1.  "Life Cover" 
        /// 2.  "Life Cover With Return Of Premium"
        /// 3.  "Life cover with critical illness"
        ///  </summary>
        [Required]
        public string? BenefitOption { get; set; }
        /// <summary>
        ///  Mapping Values for PayoutOption:
        /// 1.  "Lump-Sum" 
        /// 2.  "Monthly Income"
        /// 3.  "Lump-sum Plus Monthly Income"
        /// </summary>
        [Required]
        public string? PayoutOption { get; set; }
        /// <summary>
        ///  Mapping Values for SubDistributionChannel:
        /// 1.  "Sud Life Staff/Family" 
        /// 2.  "Direct Sales Team"
        /// </summary>
        [Required]
        public string SubDistributionChannel { get; set; }

    }
    public class ApplicantDetailsAbhay
{
        [Required]
        public string? ApplicantFName { get; set; }

        [Required]
        public string? ApplicantLName { get; set; }

        [Required]
        public string? ApplicantDateOfBirth { get; set; }

        [Required]
        public string? ApplicantGender { get; set; }

        [Required]
        public string? ApplicantEmail { get; set; }

        [Required]

        public string ApplicantContactNumber { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
    }

public class ProposerDetailsAbhay
{
        [Required]
        public string? IsProposersameasApplicant { get; set; }

        [Required]
        public string? ProposerFName { get; set; }

        [Required]
        public string? ProposerLName { get; set; }

        [Required]
        public string? ProposerDateOfBirth { get; set; }
        [Required]
        public string? ProposerGender { get; set; }
        [Required]
        public string? ProposerEmail { get; set; }
        [Required]
        public string ProposerContactNumber { get; set; }
        public string ProposerCity { get; set; }
        public string ProposerState { get; set; }
    }
    public class Dummy
    {
        public string? DummyName { get; set; } = string.Empty;
        public string? DummyValue { get; set; } = string.Empty;
    }
    public class AddField
    {
        public List<Dummy> Dummy { get; set; } = new List<Dummy>();
    }
    public class ChannelDetails
    {
        [Required]
        public string ChannelType { get; set; }
        [Required]
        public string ChannelCode { get; set; }
    }

}
