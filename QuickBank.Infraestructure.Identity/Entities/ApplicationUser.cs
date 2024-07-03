using Microsoft.AspNetCore.Identity;

namespace QuickBank.Infrastructure.Identity.Entities
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? IdCard { get; set; }
        public int Status { get; set; }

    }
}
