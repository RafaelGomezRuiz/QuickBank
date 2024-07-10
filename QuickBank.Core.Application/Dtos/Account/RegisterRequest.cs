using QuickBank.Core.Application.Enums;

namespace QuickBank.Core.Application.Dtos.Account
{
    public class RegisterRequest
    {
        public string Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? IdCard { get; set; }
        public string? Email { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
        public ERoles? UserType { get; set; }
        public double? InitialAmount { get; set; }
    }
}
