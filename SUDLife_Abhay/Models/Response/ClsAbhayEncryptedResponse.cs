namespace SUDLife_Abhay.Models.Response
{
    public class ClsAbhayEncryptedResponse
    {
    public ClsAbhayEncryptedResponse()
    {

    }
    public ClsAbhayEncryptedResponse(int StatusCode, string sourceName, string encryptResSign)
    {
        this.StatusCode = StatusCode;
        this.SourceName = sourceName;
        //  this.TranscationId = transcationId;
        this.EncryptResSign = encryptResSign;
    }
    public int StatusCode { get; set; }
    public string SourceName { get; set; } = string.Empty;
    // public string TranscationId { get; set; } = string.Empty;
    public string EncryptResSign { get; set; } = string.Empty;
}
public class BadResponse
{
    public BadResponse(int StatuCode, string Msg)
    {
        this.StatusCode = StatuCode;
        this.Message = Msg;
    }

    public int StatusCode { get; set; }
    public string Message { get; set; } = string.Empty;
}
}
