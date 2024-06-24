namespace SUDLife_Abhay.Models.Response
{
    public class ClsAbhayPlainResponse
    {
        public double ModalPremium { get; set; }

        public double Tax { get; set; }

        public double ModalPremiumwithTax { get; set; }

        public double AnnualPremium { get; set; }

        public double COVIDPremium { get; set; }

        public double COVIDTax { get; set; }

        public double CovidwithTax { get; set; }

        public double CovidAnnualPremium { get; set; }

        public double TotalPremium { get; set; }

        public double TotalTAX { get; set; }

        public double TotalPremiumwithTax { get; set; }

        public double TotalAnnualPremium { get; set; }

        public string? Message { get; set; }

        public string? Status { get; set; }

        public int TransactionId { get; set; }
    }
}
