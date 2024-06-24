using System.ComponentModel.DataAnnotations;

namespace SUDLife_Akshay.Model.Request
{
    public class ClsAkshayPlainRequest
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
        [Required]
        public int PolicyTerm { get; set; }
        [Required]
        public int PremiumPaymentTerm { get; set; }

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
        ///2.	“Direct Channel”
        ///3.	“Agency”
        ///4.	“Broker”
        ///5.	“Insurance Marketing Firm”
        ///6.	“Online”
        /// </summary>
        [Required]
        public string DistributionChannel { get; set; }
        /// <summary>
        ///  Mapping Values for StaffPolicy:
        /// 1.  "Yes" 
        /// 2.  "No"
        /// </summary>
        [Required]
        public string StaffPolicy { get; set; }
        [Required]
        public int SumAssured { get; set; }
        [Required]
        public string COVIDRiderOpted { get; set; }
        public int COVIDRiderSA { get; set; }
        public string KerelaFloodsCESSApplicable { get; set; }
        /// <summary>
        /// Mapping Values for StandardAgeProof:
        ///1.	“Yes”
        ///2.	“No” 
        /// </summary>
        [Required]
        public string StandardAgeProof { get; set; }
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
