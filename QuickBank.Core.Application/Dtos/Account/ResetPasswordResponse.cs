namespace QuickBank.Core.Application.Dtos.Account
{
    public class ResetPasswordResponse
    {
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
