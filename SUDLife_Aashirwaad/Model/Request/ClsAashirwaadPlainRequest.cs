using System.ComponentModel.DataAnnotations;

namespace SUDLife_Aashirwaad.Model.Request
{
    
    public class ClsAashirwaadPlainRequest
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
        public string? CustomerID { get; set; }
        public AddField AddField { get; set; }
        public ApplicantDetailsAashirwaad ApplicantDetails { get; set; }
        public ProposerDetailsAashirwaad ProposerDetails { get; set; }
        /// <summary> 
        ///  Mapping Values for PremiumPaymentModes:
        /// 1.  "Annual"
        /// 2.  "Semi-Annual"
        /// 3.  "Quarterly (ecs/ si)"
        /// 4.  "Monthly (ecs/ si)"
        /// </summary>
        [Required]
        public string? PremiumPaymentModes { get; set; }
        [Required]
        public string KerelaFloodsCESSApplicable { get; set; }
        /// <summary>
        ///  Mapping Values for DistributionChannel:
        ///1.	“Corporate Agents"
        ///2.	“Brokers"
        ///3.	“Agency"
        ///4.	“Direct Marketing"
        ///5.	“Online"
        /// </summary>
        [Required]
        public string? DistributionChannel { get; set; }
        /// <summary>
        ///  Mapping Values for StaffPolicy:
        /// 1.  "Yes" 
        /// 2.  "No"
        /// </summary>
        [Required]
        public string? StaffPolicy { get; set; }
        [Required]
        public int AnnualPremium { get; set; }
        [Required]
        public int SumAssured { get; set; }
        /// <summary>
        /// Mapping Values for StandardAgeProof:
        ///1.	“Yes”
        ///2.	“No” 
        /// </summary>
        [Required]
        public string StandardAgeProof { get; set; }
        /// <summary>
        ///  Mapping Values for PayoutOption:
        /// 1.  "Self Starter" 
        /// 2.  "Professional"
        /// 3.  "Foundation"
        /// 4.  "Technical"
        /// 5.  "Career Builder"
        /// </summary>
        [Required]
        public string PayoutOption { get; set; }

        [Required]
        public int ADTPDRiderSA { get; set; }
        [Required]
        public string ADTPDRiderOpted { get; set; }
        [Required]
        public string FIBRiderOpted { get; set; }
        [Required]
        public int FIBRIderSA { get; set; }
        /// <summary>
        /// PolicyTerm have following Mapping Values:
        ///1.	"120" 
        ///2.   "132" 
        ///3.	"144" 
        ///4.	"156" 
        ///5.	"168" 
        ///6.	"180" 
        ///7.	"192" 
        ///8.	"204" 
        ///9.	"216" 
        ///10.	"228"
        ///11.  "240"
        /// </summary>
        [Required]
        public int PolicyTerm { get; set; }

        /// <summary>
        /// PremiumPaymentTerm have following Mapping Values:
        /// 1. "60"
        /// 2. "84" 
        /// 3. "120"
        /// 4. "180" 
        /// </summary>
        [Required]
        public int PremiumPaymentTerm { get; set; }

    }





    public class ApplicantDetailsAashirwaad
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
        public string? ApplicantContactNumber { get; set; }
        public string ApplicantCity { get; set; }
        public string ApplicantState { get; set; }
    }

    public class ProposerDetailsAashirwaad
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
        public string DummyName { get; set; } = string.Empty;
        public string DummyValue { get; set; } = string.Empty;
    }

    public class AddField
    {
        public List<Dummy> Dummy { get; set; } = new List<Dummy>();
    }
}
