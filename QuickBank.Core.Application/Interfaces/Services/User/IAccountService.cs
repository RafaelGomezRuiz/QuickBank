using QuickBank.Core.Application.Dtos.Account;

namespace QuickBank.Core.Application.Interfaces.Services.User
{
    public interface IAccountService
    {
        Task<IEnumerable<AuthenticationResponse>> GetAllAsync();
        Task<AuthenticationResponse> FindByIdAsync(string userId);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        Task<bool> DuplicateUserName(string userName);
        Task<bool> DuplicateEmail(string email);
        Task SignOutAsync();
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);


        Task<ForgotPasswordResponse> ForgotPasswordAsync(ForgotPasswordRequest request, string origin);
        Task<AuthenticationResponse> UpdateUserAsync(AuthenticationResponse user);
        Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request);

    }
}
