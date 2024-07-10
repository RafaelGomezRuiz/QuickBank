using QuickBank.Core.Application.Enums;
using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public string? Id { get; set; }

        [Required(ErrorMessage = "The name field cannot be empty")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The lastname field cannot be empty")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The idcard field cannot be empty")]
        [DataType(DataType.Text)]
        public string IdCard { get; set; }

        [Required(ErrorMessage = "The Email field cannot be empty")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "The username field cannot be empty")]
        [DataType(DataType.Text)]
        public string UserName { get; set; }

        [Required(ErrorMessage = "The password field cannot be empty")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Required(ErrorMessage = "The confirm password field cannot be empty")]
        [Compare(nameof(Password))]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Select a valid option")]
        [DataType(DataType.Text)]
        public ERoles UserType { get; set; }

        [DataType(DataType.Currency)]
        public double? InitialAmount { get; set; }
        public int? Status { get; set; }
        public bool HasError { get; set; }
        public string? ErrorDescription { get; set; }
    }
}
