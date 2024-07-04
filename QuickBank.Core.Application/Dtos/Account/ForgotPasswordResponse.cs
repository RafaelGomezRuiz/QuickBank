namespace QuickBank.Core.Application.Dtos.Account
{
    public class ForgotPasswordResponse
    {
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
