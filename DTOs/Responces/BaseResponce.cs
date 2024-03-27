using System.Net;

namespace SuperHeros.DTOs.Responces
{
    public class BaseResponce
    {
        public int status { get; set; }
        public object data { get; set; }

        public void CreateResponse(HttpStatusCode statusCode, object data)
        {
            status = (int)statusCode;
            this.data = data;
        }
    }
}
