using System.Net;

namespace WebSakila.Models
{
    public class APIResponse
    {
        public HttpStatusCode statusCode { get; set; }
        public bool IsSuccessful { get; set; } = true;
        public List<string> ErrorMessages { get; set; }
        public object Result { get; set; }
      
    }
}
