namespace SUDLife_AssuredIncome.Model.Response
{
    public class ClsAssuredIncomePlainResponse
    {
        public double SumAssured { get; set; }

        public double AnnualPremium { get; set; }

        public double ModalPremium { get; set; }

        public double Tax { get; set; }

        public double ModalPremiumwithTax { get; set; }

        public double ADTPDPremium { get; set; }

        public double ADTPDTax { get; set; }

        public double ADTPDwithTax { get; set; }

        public double ADTPDAnnualPremium { get; set; }


        public double COVIDPremium { get; set; }

        public double COVIDTax { get; set; }

        public double CovidwithTax { get; set; }

        public double COVIDAnnualPremium { get; set; }

        public double TotalPremium { get; set; }

        public double TotalTAX { get; set; }

        public double TotalPremiumwithTax { get; set; }

        public double TotalAnnualPremium { get; set; }


        public object Message { get; set; }

        public string Status { get; set; }

        public int TransactionId { get; set; }
    }
}
