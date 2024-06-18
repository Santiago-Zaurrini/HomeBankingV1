
namespace HomeBanking.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode;
        public string message;
        public CustomException(string _message, int _statusCode) : base(_message)
        {
            StatusCode = _statusCode;
            message = _message;
        }
    }
}
