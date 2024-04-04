using static UtilitySakila.DS;

namespace WebSakila.Models
{
    public class APIRequest
    {
        public APIType APIType { get; set; } = APIType.GET;

        public string Url { get; set; }

        public object Data { get; set; }

    }
}
