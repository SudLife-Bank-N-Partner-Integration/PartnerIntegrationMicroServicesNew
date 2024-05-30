using RestSharp;

namespace SUDLife_CallThirdPartyAPI
{
    public class ThirdPartyAPI
    {
        public int id { get; set; } = 1;
        public string ClientAPI(string url,Method method,string? body = null)
        {
            var client = new RestClient();
            var request= new RestRequest(url,method);

            request.AddHeader("Content-Type", "application/json");
            
            if(method == Method.Post)
                request.AddJsonBody(body);

            var response = client.Execute<RestResponse>(request).Content.ToString(); 

            return response;
        }
    }
}
