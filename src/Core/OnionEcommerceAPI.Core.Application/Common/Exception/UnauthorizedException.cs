namespace OnionEcommerceAPI.Core.Application.Common.Exception
{
    public class UnauthorizedException : ApplicationException
    {
        public UnauthorizedException(string message)
            : base(message)
        {

        }
    }
}
