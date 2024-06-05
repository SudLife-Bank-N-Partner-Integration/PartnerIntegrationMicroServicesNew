namespace SUDLife_Aadarsh.Models.Response
{
    public class ClsAadarshEncryptedResponse
    {
       
        public ClsAadarshEncryptedResponse()
        {

        }
        public ClsAadarshEncryptedResponse(int StatusCode, string sourceName, string encryptResSign)
        {
            this.StatusCode = StatusCode;
            this.SourceName = sourceName;
            //  this.TranscationId = transcationId;
            this.EncryptResSign = encryptResSign;
        }
        public int StatusCode { get; set; }
        public string? SourceName { get; set; }
        // public string? transcationId { get; set; }
        public string? EncryptResSign { get; set; }
    }


}
