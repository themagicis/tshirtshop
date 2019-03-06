using TShirtShop.Services.Constants;

namespace TShirtShop.Services.Results
{
    public class ResultError
    {
        public ResultError(string code)
        {
            this.Code = code;
        }

        public string Code { get; set; }

        public string Description => ErrorMessages.Errors[this.Code];
    }
}