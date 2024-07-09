using System.Net;

namespace Blue.Challenge.Business.Responses
{
    public class RequestHandlerResponse 
    {
        public object Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public string ErrorMessage { get; set; }
        public RequestHandlerResponse(object data, HttpStatusCode statusCode)
        {
            Data = data;
            StatusCode = statusCode;
        }

        public RequestHandlerResponse(HttpStatusCode statusCode)
        {
            StatusCode = statusCode;
        }
    }
}
