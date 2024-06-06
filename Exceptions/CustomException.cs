using System.Net;

namespace HomeBanking.Exceptions
{
    public class CustomException : Exception
    {
        public HttpStatusCode StatusCode;

        public CustomException(string message, HttpStatusCode _statusCode) : base(message)
        {
            StatusCode = _statusCode;
        }
    }
}
