using QuickBank.Core.Application.Dtos.Account;

namespace QuickBank.Core.Application.Interfaces.Services.User
{
    public interface IAccountService
    {
        Task<IEnumerable<AuthenticationResponse>> GetAllAsync();
        Task<AuthenticationResponse> FindByIdAsync(string id);
        Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request);
        //Task<string> ConfirmAccountAsync(string userId, string token);
        Task<AuthenticationResponse> UpdateUserAsync(AuthenticationResponse user);
        Task<RegisterResponse> RegisterUserAsync(RegisterRequest request);
        Task SignOutAsync();
        Task<bool> DuplicateUserName(string userName);
        Task<bool> DuplicateEmail(string email);
    }
}
