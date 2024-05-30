namespace SUDLife_Aadarsh.Models.Request
{
    public class ClsAadarshPlainRequest
    {
        public string? InwardDate { get; set; }
        public string? ApplicationNo { get; set; }
        public string? Source { get; set; }
        public string? CustomerID { get; set; }
        public Addfield? AddField { get; set; }
        public Applicantdetails? ApplicantDetails { get; set; }
        public Proposerdetails? ProposerDetails { get; set; }
        public string? StandardAgeProof { get; set; }
        public string? PremiumPaymentModes { get; set; }
        public string? KerelaFloodsCESSApplicable { get; set; }
        public string? DistributionChannel { get; set; }
        public string? SumAssured { get; set; }
        public string? StaffPolicy { get; set; }
    }
    public class Addfield
    {
        public Dummy[]? Dummy { get; set; }
    }
    public class Dummy
    {
        public string? DummyName { get; set; }
        public string? DummyValue { get; set; }
    }
    public class Applicantdetails
    {
        public string? ApplicantFName { get; set; }
        public string? ApplicantLName { get; set; }
        public string? ApplicantDateOfBirth { get; set; }
        public string? ApplicantGender { get; set; }
        public string? ApplicantEmail { get; set; }
        public string? ApplicantContactNumber { get; set; }
        public string? ApplicantCity { get; set; }
        public string? ApplicantState { get; set; }
    }
    public class Proposerdetails
    {
        public string? IsProposersameasApplicant { get; set; }
        public string? ProposerFName { get; set; }
        public string? ProposerLName { get; set; }
        public string? ProposerDateOfBirth { get; set; }
        public string? ProposerGender { get; set; }
        public string? ProposerEmail { get; set; }
        public string? ProposerContactNumber { get; set; }
        public string? ProposerCity { get; set; }
        public string? ProposerState { get; set; }
    }

}
