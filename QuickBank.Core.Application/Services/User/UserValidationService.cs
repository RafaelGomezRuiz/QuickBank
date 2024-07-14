using QuickBank.Core.Application.Helpers;
using QuickBank.Core.Application.Interfaces.Services.User;
using QuickBank.Core.Application.ViewModels.User;
using System.Text.RegularExpressions;

namespace QuickBank.Core.Application.Services.User
{
    public class UserValidationService : IUserValidationService
    {
        protected readonly IAccountService accountService;
        protected readonly IUserService userService;

        public UserValidationService(IAccountService accountService, IUserService userService)
        {
            this.accountService = accountService;
            this.userService = userService;
        }

        public async Task<Dictionary<string, string>> UserSaveValidation(UserSaveViewModel userSaveViewModel)
        {
            var errors = new Dictionary<string, string>();
            if (userSaveViewModel.Email != null)
            {
                bool duplicateEmail = await userService.DuplicateEmail(userSaveViewModel.Email);
                if (duplicateEmail) errors.Add("DuplicateEmail", "Email already in use");
            }
            if (userSaveViewModel.UserName != null)
            {
                bool duplicateUserName = await userService.DuplicateUserName(userSaveViewModel.UserName);
                if (duplicateUserName) errors.Add("DuplicateUserName", "Username already in use");
            }
            bool initialAmountNull = (userSaveViewModel.InitialAmount == null ? true : false);
            if (initialAmountNull) errors.Add("InitialAmountNull", "Debes introducir un valor numerico");

            bool invalidUserType = (userSaveViewModel.UserType.ToString() == string.Empty ? true : false);
            if (invalidUserType) errors.Add("InvalidUserType", "Debes eligir un rol valido");

            return errors;
        }
        public async Task<Dictionary<string, string>> PasswordValidation(UserSaveViewModel userSaveViewModel)
        {
            var errors = new Dictionary<string, string>();
            var lowerCasePattern = @"[a-z]";
            var upperCasePattern = @"[A-Z]";
            var hasNumberPattern = @"\d";
            var nonAlphanumericPattern = @"\W";

            if (userSaveViewModel.Password == null)
                errors.Add("PasswordRequired", $"The password is required");
            
            if (userSaveViewModel.Password != null)
            {
                // Check if password meets the minimum length requirement
                if (userSaveViewModel.Password.Length < BusinessLogicConstantsHelper.MinPasswordLength)
                    errors.Add("MinPasswordLength", $"The minimum length is {BusinessLogicConstantsHelper.MinPasswordLength}");

                // Check if password contains at least one lowercase letter
                if (!Regex.IsMatch(userSaveViewModel.Password, lowerCasePattern))
                    errors.Add("LowerCase", "Password must contain at least one lowercase letter");

                // Check if password contains at least one uppercase letter
                if (!Regex.IsMatch(userSaveViewModel.Password, upperCasePattern))
                    errors.Add("UpperCase", "Password must contain at least one uppercase letter");

                // Check if password contains at least one digit
                if (!Regex.IsMatch(userSaveViewModel.Password, hasNumberPattern))
                    errors.Add("RequireDigit", "Password needs digts[1234567890]");

                // Check if password contains at least one non-alphanumeric character
                if (!Regex.IsMatch(userSaveViewModel.Password, nonAlphanumericPattern))
                    errors.Add("RequireNonAlphanumeric", "Password must contain especial character[_,#@$]");
            }

            return errors;
        }
    }
}
