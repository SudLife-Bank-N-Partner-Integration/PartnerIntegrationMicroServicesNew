namespace SUDLife_Aadarsh.Models.Response
{
    public class ClsAadarshPlainResponse
    {
        public double ModalPremium { get; set; }
        public double Tax { get; set; }
        public double ModalPremiumwithTax { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public int TransactionId { get; set; }
    }
}
