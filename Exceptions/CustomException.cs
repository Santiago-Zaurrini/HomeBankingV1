
namespace HomeBanking.Exceptions
{
    public class CustomException : Exception
    {
        public int StatusCode;

        public CustomException(string message, int _statusCode) : base(message)
        {
            StatusCode = _statusCode;
        }
    }
}
