namespace QuickBank.Core.Application.Dtos.Account
{
    public class ResetPasswordRequest
    {
        //enum caso de que sea por email se cambia
        public string? Email { get; set; }
        public string? Token { get; set; }
        public string? Password { get; set; }
    }
}
