namespace SUDLife_Authentication.Models.Request
{
    public class MstSecretKey
    {
        public int ID { get; set; }
        public string Partner { get; set; }
        public string SecretKey { get; set; }
        public bool Active { get; set; }
    }
}
