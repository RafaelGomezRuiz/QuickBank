namespace QuickBank.Core.Application.Dtos.Account
{
    public class ForgotPasswordRequest
    {
        public string? Email { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
