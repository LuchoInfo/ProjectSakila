using System.Net;

namespace ApiSakila.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;

        public List<string> ErrorMessages { get; set; }

        public object Result { get; set; }
    }
}
