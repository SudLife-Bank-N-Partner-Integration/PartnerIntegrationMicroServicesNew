namespace SUDLife_Aashirwaad.Model.Response
{
    public class ClsAashirwaadPlainResponse
    {
        public double ModalPremium { get; set; }
        public double Tax { get; set; }
        public double ModalPremiumwithTax { get; set; }
        public double AnnualPremium { get; set; }
        public double TotalPremium { get; set; }
        public double TotalTAX { get; set; }
        public double TotalPremiumwithTax { get; set; }
        public double TotalAnnualPremium { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        public int TransactionId { get; set; }
        public double SumAssured { get; set; }
        public double ADTPDPremium { get; set; }
        public double ADTPDTax { get; set; }
        public double ADTPDwithTax { get; set; }
        public double ADTPDAnnualPremium { get; set; }
        public double FIBPremium { get; set; }
        public double FIBTax { get; set; }
        public double FIBwithTax { get; set; }
        public double FIBAnnualPremium { get; set; }
    }
}
