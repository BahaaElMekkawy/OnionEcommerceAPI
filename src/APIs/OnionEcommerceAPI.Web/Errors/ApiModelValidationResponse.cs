namespace OnionEcommerceAPI.Web.Errors
{
    public class ApiModelValidationResponse : ApiResponse
    {
        public required IEnumerable<string> Errors { get; set; }

        public ApiModelValidationResponse(string? message = null)
            : base(400, message)
        {

        }
    }
}
