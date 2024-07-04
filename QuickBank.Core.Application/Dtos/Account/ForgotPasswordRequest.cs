namespace QuickBank.Core.Application.Dtos.Account
{
    public class ForgotPasswordRequest
    {
        public string? UserName { get; set; }
        public string Token { get; set; }
        public string Password { get; set; }
    }
}
