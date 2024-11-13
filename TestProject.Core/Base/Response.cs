using System.Net;

namespace TestProjectZletano.Core.Base
{
    public class Response<T>
    {
        public Response()
        {
            Errors = new List<string>();
        }

        public Response(T data, string message = null)
        {
            Succeeded = true;
            Message = message;
            Data = data;
            Errors = new List<string>();
            StatusCode = HttpStatusCode.OK; // يمكنك تغيير هذا بناءً على احتياجاتك
        }

        public Response(string message)
        {
            Succeeded = false;
            Message = message;
            Errors = new List<string>();
            StatusCode = HttpStatusCode.BadRequest; // يمكنك تغيير هذا بناءً على احتياجاتك
        }

        public Response(string message, bool succeeded)
        {
            Succeeded = succeeded;
            Message = message;
            Errors = new List<string>();
            StatusCode = succeeded ? HttpStatusCode.OK : HttpStatusCode.BadRequest; // تغيير الحالة بناءً على النجاح
        }

        public HttpStatusCode StatusCode { get; set; }
        public object Meta { get; set; }

        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public List<string> Errors { get; set; }
        public T Data { get; set; }
    }
}