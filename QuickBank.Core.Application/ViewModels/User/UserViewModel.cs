namespace QuickBank.Core.Application.ViewModels.User
{
    public class UserViewModel
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdCard { get; set; }
        public string UserName { get; set; }
        public string? Password { get; set; }
        public string? PhoneNumber { get; set; }
        public List<string> Roles { get; set; }
        public bool Status { get; set; }
    }
}
