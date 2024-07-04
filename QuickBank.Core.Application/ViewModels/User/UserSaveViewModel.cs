using System.ComponentModel.DataAnnotations;

namespace QuickBank.Core.Application.ViewModels.User
{
    public class UserSaveViewModel
    {
        public string Id { get; set; }

        [Required(ErrorMessage = "The name field cannot be empty")]
        [DataType(DataType.Text)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "The lastname field cannot be empty")]
        [DataType(DataType.Text)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The idcard field cannot be empty")]
        [DataType(DataType.Text)]
        public string IdCard { get; set; }

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
        [Range(1,int.MaxValue)]
        public int UserType { get; set; }

        [Required(ErrorMessage = "The initial amount field cannot be empty")]
        [DataType(DataType.Currency)]
        public double? InitialAmount { get; set; }
    }
}
