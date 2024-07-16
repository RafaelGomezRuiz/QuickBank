namespace QuickBank.Core.Application.Dtos.Account
{
    public class RegisterResponse
    {
        public string? Id { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
