using QuickBank.Core.Application.Dtos.Account;
using QuickBank.Core.Application.ViewModels.Auth;
using QuickBank.Core.Application.ViewModels.User;

namespace QuickBank.Core.Application.Interfaces.Services.User
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync();
        Task<UserSaveViewModel> FindyByIdAsync(string id);
        Task<RegisterResponse> RegisterAsync(UserSaveViewModel vm, string origin);
        Task<UserSaveViewModel> UpdateUserAsync(UserSaveViewModel saveUserViewModel);
        Task SignOutAsync();
        //Task<string> ConfirmEmailAsync(string userId, string token);
        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordViewModel forgotPasswordVm, string origin);
        Task<AuthenticationResponse> LoginAsync(LoginViewModel vm);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordViewModel resetPassword);
        Task<IEnumerable<UserViewModel>> GetActiveUsersAsync();
        Task<IEnumerable<UserViewModel>> GetInactiveUsersAsync();
        Task<bool> DuplicateUserName(string userName);
        Task<bool> DuplicateEmail(string email);
    }
}
