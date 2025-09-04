using System.Net;

namespace SchoolPrj.Core.Bases
{
    public class Response<T>
    {
        public Response()
        {

        }
        public Response(T data, string message = null)
        {
            succeeded = true;
            Message = message;
            Data = data;
        }
        public Response(string message)
        {
            succeeded = false;
            Message = message;
        }
        public Response(string message, bool succeeded)
        {
            succeeded = succeeded;
            Message = message;
        }
        public bool succeeded { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }
        public List<string> Errors { get; set; }
    }
}
