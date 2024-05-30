namespace SUDLife_Aadarsh.Models.Response
{
    public class ClsAadarshPlainResponse
    {
        public float ModalPremium { get; set; }
        public float Tax { get; set; }
        public float ModalPremiumwithTax { get; set; }
        public string? Message { get; set; }
        public string? Status { get; set; }
        public int TransactionId { get; set; }
    }
}
